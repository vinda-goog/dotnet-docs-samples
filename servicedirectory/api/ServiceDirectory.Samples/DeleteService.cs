/*
 * Copyright 2020 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// [START servicedirectory_delete_service]

using Google.Cloud.ServiceDirectory.V1Beta1;

public class DeleteServiceSample
{
    public void DeleteService(string projectId = "my-project",
        string locationId = "us-east1",
        string namespaceId = "test-namespace",
        string serviceId = "test-service")
    {
        // Create client
        RegistrationServiceClient registrationServiceClient = RegistrationServiceClient.Create();
        // Initialize request argument(s)
        DeleteServiceRequest request = new DeleteServiceRequest
        {
            ServiceName = ServiceName.FromProjectLocationNamespaceService(projectId, locationId, namespaceId, serviceId),
        };
        // Make the request
        registrationServiceClient.DeleteService(request);
        // End snippet
    }
}

// [END servicedirectory_delete_service]