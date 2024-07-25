using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using GestaoProduto.Application.DTOs;
using GestaoProduto.Application.Models;
using GestaoProduto.Core.Entities;

namespace GestaoProduto.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoDto, Produto>();
            CreateMap<FornecedorDto, Fornecedor>();

            CreateMap<Produto, ProdutoViewModel>();
            CreateMap<Fornecedor, FornecedorViewModel>();
        }
    }
}