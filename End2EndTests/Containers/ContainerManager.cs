using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeleniumTests
{
    internal class ContainerManager
    {
        static List<string> containerIds = new List<string>();

        /// <summary>
        /// create the website based on a container image name "globoticket" that is asumed to be available on the host
        /// </summary>
        /// <param name="containerName">name you want to give to the container</param>
        /// <returns>The IP Adress the website can be found and the container id (string guid)</returns>
        internal static async Task<Tuple<string, string>> StartWebsite(string containerName)
        {
            var musicstoreWeb_id = await CreateContainerFromImageOnPort(containerName, "globoticket", null, "80", "80");

            DockerClient client = new DockerClientConfiguration(
                       new Uri("npipe://./pipe/docker_engine"))
                       .CreateClient();

            var musicstoreDetails = await client.Containers.InspectContainerAsync(musicstoreWeb_id);
            return new Tuple<string, string>(musicstoreDetails.NetworkSettings.Networks["nat"].IPAddress, musicstoreWeb_id);
        }

        internal static async Task<string> StartCleanCatalogDatabaseContainer(string containerName)
        {
            var environment = new List<string>() { "MSSQL_SA_PASSWORD=myPassword123!@", "ACCEPT_EULA=Y" };
            var catalog_id = await CreateContainerFromImageOnPort(containerName, "mcr.microsoft.com/mssql/server:latest", environment, "1433", "1433");
            return catalog_id;
        }

        internal static async Task<string> StartStateOfTranceDatabaseContainer(string containerName)
        {
            var catalog_id = await CreateContainerFromImageOnPort(containerName, "marcelv/globoticket-stateoftrance-db", null, "1433", "1433");
            return catalog_id;
        }
        internal static async Task<string> StartDefaultDatabaseContainer(string containerName)
        {
            var catalog_id = await CreateContainerFromImageOnPort(containerName, "marcelv/globoticket-default-db", null, "1433", "1433");
            return catalog_id;
        }
        internal static async Task<bool> RemoveContainerWithName(string containerName)
        {
            DockerClient client = new DockerClientConfiguration(
                     new Uri("npipe://./pipe/docker_engine"))
                     .CreateClient();

            var containerList = client.Containers.ListContainersAsync(new ContainersListParameters() { }).Result;

            foreach (var container in containerList)
            {

                if (container.Names.Contains("/" + containerName))
                {
                    var removeParams = new ContainerRemoveParameters() { Force = true };
                    await client.Containers.RemoveContainerAsync(container.ID, removeParams);
                    return true;
                }
            }
            return false;
        }

        internal async static Task<bool> ExecuteCommandInContainer(string containerName, List<string> commandsToExecute)
        {
            DockerClient client = new DockerClientConfiguration(
                    new Uri("npipe://./pipe/docker_engine"))
                    .CreateClient();
            var id = GetContainerIdByName(containerName);
            if (id != null)
            {
                var response = await client.Containers.CreateContainerAsync(
                    new CreateContainerParameters()
                {
                     Cmd = commandsToExecute,
                      
                } );
                

                await client.Containers.StartContainerAsync(id,new ContainerStartParameters());
                return true;
            }
            else
                return false;

        }

        private static async Task<string> CreateContainerFromImageOnPort(string containerName, string imageName, IList<string> environment = null, string containerPort = null, string exposedPort = null)
        {
            DockerClient client = new DockerClientConfiguration(
                        new Uri("npipe://./pipe/docker_engine"))
                        .CreateClient();
            EmptyStruct mystruckt;

            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Name = containerName,
                Image = imageName,
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                      {
                         { containerPort, new List<PortBinding> { new PortBinding { HostPort= exposedPort } } }
                      }
                }
                ,
                ExposedPorts = new Dictionary<string, EmptyStruct> { { exposedPort, mystruckt } },
                Env = environment,


            }
            );

            await client.Containers.StartContainerAsync(response.ID,
                new ContainerStartParameters()
                {

                });
            //register the container as created so we can clean up later
            containerIds.Add(response.ID);
            return response.ID;
        }

        internal static async Task CleanupContainers()
        {
            DockerClient client = new DockerClientConfiguration(
                        new Uri("npipe://./pipe/docker_engine"))
                        .CreateClient();

            foreach (string container_id in containerIds)
            {
                await RemoveContainerWithId(container_id);
            }
        }

        internal static async Task<bool> RemoveContainerWithId(string containerId)
        {
            DockerClient client = new DockerClientConfiguration(
                     new Uri("npipe://./pipe/docker_engine"))
                     .CreateClient();

            var containerList = client.Containers.ListContainersAsync(new ContainersListParameters() { }).Result;

            foreach (var container in containerList)
            {

                if (container.ID == containerId)
                {
                    var removeParams = new ContainerRemoveParameters() { Force = true };
                    await client.Containers.RemoveContainerAsync(container.ID, removeParams);
                    return true;
                }
            }
            return false;
        }

        private static string GetContainerIdByName(string containerName)
        {
            DockerClient client = new DockerClientConfiguration(
                     new Uri("npipe://./pipe/docker_engine"))
                     .CreateClient();

            var containerList = client.Containers.ListContainersAsync(new ContainersListParameters() { }).Result;

            foreach (var container in containerList)
            {

                if (container.Names.Contains("/" + containerName))
                {
                    return container.ID; ;
                }
            }
            return null;
        }
    }
}