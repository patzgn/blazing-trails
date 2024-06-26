﻿using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails.AddTrail;

public class AddTrailHandler : IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
{
    private readonly HttpClient _httpClient;

    public AddTrailHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);

            return new AddTrailRequest.Response(trailId);
        }

        return new AddTrailRequest.Response(-1);
    }
}
