# PARCS-NET-K8

## Overview
PARCS-NET-K8 is a solution for deploying and managing algorithmic modules that solve recursive parallel computation problems. This repository contains the necessary files and configurations to deploy the solution onto Azure as an AKS (Azure Kubernetes Service) service. It also provides a local development environment using Docker Compose.

## Deployment

To deploy the solution onto Azure as an AKS service, follow these steps:

1. Create a Kubernetes cluster (AKS) on Azure using the Azure Portal. This will provision the necessary resources for the AKS cluster. Make sure to configure the cluster according to your requirements.

2. Apply the YAML file located at `kube/deployment.azure.yaml` to configure the AKS cluster. This file specifies the desired state of the cluster and sets up the necessary configurations.

## Local Development
For local development and debugging, you can use Docker Compose. Follow these steps:

1. Ensure you have Docker Compose installed on your local machine.

2. Use the `src/docker-compose.yml` file to set up the local development environment. This file defines the necessary services and their configurations.

3. Build and run the Docker Compose setup using the following command:
   ```
   docker-compose up
   ```

   This will create the required containers and start the local development environment.

## NuGet Package
The solution depends on a NuGet package named "Parcs.Net" (version 4.0.0). You can find the package on NuGet.org at the following URL: [https://www.nuget.org/packages/Parcs.Net/](https://www.nuget.org/packages/Parcs.Net/)

Make sure to include this package in your project to utilize the algorithmic modules provided by the solution.

## Contributing
Contributions to the PARCS-NET-K8 solution are welcome! If you encounter any issues or have suggestions for improvements, please open an issue or submit a pull request in this repository.

## License
This repository is licensed under the [MIT License](LICENSE). Feel free to use and modify the code as per the terms of the license.

## Acknowledgements
We would like to thank the contributors and maintainers of the PARCS-NET-K8 solution. Your efforts and support are greatly appreciated.
