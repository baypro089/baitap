﻿using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class BookCatalogueMapper : Profile
    {
        public BookCatalogueMapper() {
            CreateMap<BookCatalogue, BookCatalogueDTO>();
            CreateMap<BookCatalogueDTO, BookCatalogue>();
        }
    }
}