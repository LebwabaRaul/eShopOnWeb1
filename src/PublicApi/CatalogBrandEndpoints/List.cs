﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class List : BaseAsyncEndpoint<ListCatalogBrandsResponse>
    {
        private readonly IAsyncRepository<CatalogBrand> _catalogBrandRepository;
        private readonly IMapper _mapper;

        public List(IAsyncRepository<CatalogBrand> catalogBrandRepository,
            IMapper mapper)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        [HttpGet("api/catalog-brands")]
        [SwaggerOperation(
            Summary = "List Catalog Brands",
            Description = "List Catalog Brands",
            OperationId = "catalog-brands.List",
            Tags = new[] { "CatalogBrandEndpoints" })
        ]
        public override async Task<ActionResult<ListCatalogBrandsResponse>> HandleAsync()
        {
            var response = new ListCatalogBrandsResponse();

            var items = await _catalogBrandRepository.ListAllAsync();

            response.CatalogBrands.AddRange(items.Select(_mapper.Map<CatalogBrandDto>));

            return Ok(response);
        }
    }
}
