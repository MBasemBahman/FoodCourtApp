global using AutoMapper;
global using Entities.Constants;
global using DAL;
global using Entities;
global using Entities.AuthenticationModels;
global using Entities.ServicesModels;
global using Microsoft.AspNetCore.Localization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Newtonsoft.Json.Converters;
global using Repository;
global using Services;
global using System.Globalization;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq.Dynamic.Core;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using BC = BCrypt.Net.BCrypt;
global using Dashboard.Helpers;
global using Dashboard.Services;
global using Dashboard.ViewModel;
global using Entities.DtoModels.AppModels;
global using Entities.Features.RequestFeatures;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using Entities.DBModels.AuthModels;
global using Entities.DBModels.AppModels;
global using Entities.DBModels.ShopModels;
global using Entities.DtoModels.ShopModels;
global using Entities.DtoModels.AuthModels;
global using Entities.Extensions;











