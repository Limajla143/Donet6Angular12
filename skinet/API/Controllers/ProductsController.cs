﻿using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductType> productTypesRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> _products, IGenericRepository<ProductType> _productTypes, IGenericRepository<ProductBrand> _productBrand,
            IMapper _mapper)
        {
            productsRepo = _products;
            productTypesRepo = _productTypes;
            productBrandRepo = _productBrand;
            mapper = _mapper;
        }
       [HttpGet]
       public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams prodParams)
        {
            var spec = new ProductsTypeBrandSpec(prodParams);
            var countSpec = new ProductFilterCountSpecs(prodParams);
            var totalItems = await productsRepo.CountAsync(countSpec);
            var products = await productsRepo.ListAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            

            return Ok(new Pagination<ProductToReturnDto>(prodParams.PageIndex, prodParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),  StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsTypeBrandSpec(id);

            var product = await productsRepo.GetEntityWithSpec(spec);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await productTypesRepo.ListAllAsync());
        }
    }
}
