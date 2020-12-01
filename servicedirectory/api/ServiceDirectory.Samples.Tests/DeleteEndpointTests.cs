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
 
using Google.Cloud.ServiceDirectory.V1Beta1;
using System;
using Grpc.Core;
using Xunit;

[Collection(nameof(ServiceDirectoryFixture))]

public class DeleteEndpointTest : IDisposable
{ 
    private readonly ServiceDirectoryFixture _fixture;
    private readonly DeleteEndpointSample _sample;
    private string _namespaceId;
 
    public DeleteEndpointTest(ServiceDirectoryFixture fixture)
    {
        _fixture = fixture;
        _sample = new DeleteEndpointSample();
    }

    public void Dispose()
    {
        _fixture.DeleteNamespace(_namespaceId);
    }

    [Fact]
    public void DeletesEndpoint()
    {
        // Setup namespace, service, and endpoint for the test.
        _namespaceId = _fixture.RandomResourceId();
        var serviceId = _fixture.RandomResourceId();
        var endpointId = _fixture.RandomResourceId();
        _fixture.CreateNamespace(_namespaceId);
        _fixture.CreateService(_namespaceId, serviceId);
        _fixture.CreateEndpoint(_namespaceId, serviceId, endpointId);
        // Run the sample code.
        _sample.DeleteEndpoint(projectId: _fixture.ProjectId,
            locationId: _fixture.LocationId, namespaceId: _namespaceId, serviceId: serviceId, endpointId: endpointId);
        
        // Try to get the endpoint.
        RegistrationServiceClient registrationServiceClient = RegistrationServiceClient.Create();
        string resourceName =
            $"projects/{_fixture.ProjectId}/locations/{_fixture.LocationId}/namespaces/{_namespaceId}/services/{serviceId}";
        try
        {
            registrationServiceClient.GetService(resourceName);
        }
        catch (Grpc.Core.RpcException exception)
        {
            Assert.Equal(StatusCode.NotFound, exception.StatusCode);
        }
    }
}