using AutoMapper;
using CoffeeShop.Aplication.CoffeeOperations.Command.CreateCoffee;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetCoffeeByName;
using CoffeeShop.Aplication.CoffeeOperations.Queries.GetsCoffee;
using CoffeeShop.Aplication.CustomerOperations.Command.CreateCustomer;
using CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomerByName;
using CoffeeShop.Aplication.CustomerOperations.Queries.GetCustomers;
using CoffeeShop.Aplication.OrderOperations.Command.CreateOrder;
using CoffeeShop.Aplication.OrderOperations.Command.UpdateOrder;
using CoffeeShop.Aplication.OrderOperations.Queries.GetOrderByID;
using CoffeeShop.Aplication.OrderOperations.Queries.GetOrders;
using CoffeeShop.Entities;

namespace CoffeeShop.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            //coffeeMap
            CreateMap<CreateCoffeeModel, Coffee>();

            CreateMap<Coffee,GetCoffeeModel>() // GetsCoffees
                .ForMember(dest=>dest.genre , opt=>opt.MapFrom(src=>src.GenreCoffee.Name));

            CreateMap<Coffee,GetCoffeeByNameModel>() //getcoffeebyName
                .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.GenreCoffee.Name));


            //customerMap
            CreateMap<CreateCustomerModel, Customer>().ReverseMap(); // create

            CreateMap<Customer,GetCustomersModel>() // getsCustomers
                .ForMember(dest=>dest.purchasedCoffees,opt=>opt.MapFrom(src=>src.purchasedCoffees.Select(pm=>pm.Coffee.CoffeeName)))
                .ForMember(dest => dest.favoriteGenres, opt => opt.MapFrom(src => src.favoriteGenres.Select(fg => fg.GenreCoffee.Name)));

            CreateMap<Customer, GetCustomerByNameModel>()  // GetCustomerByName
              .ForMember(dest => dest.purchasedCoffees, opt => opt.MapFrom(src => src.purchasedCoffees.Select(pm => pm.Coffee.CoffeeName)))
              .ForMember(dest => dest.favoriteGenres, opt => opt.MapFrom(src => src.favoriteGenres.Select(fg => fg.GenreCoffee.Name)));

            //OrderMap
            CreateMap<CreateOrderModel, Order>().ForMember(dest => dest.Coffees, opt => opt.Ignore()); // create
            CreateMap<UpdateOrderModel, Order>(); // update

            CreateMap<Order, GetOrdersModel>() //gets
                .ForMember(dest => dest.customers, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname + " -- email :" + src.Customer.Email))
               .ForMember(dest => dest.Coffees, opt => opt.MapFrom(src => src.Coffees.Select(ma => $"{ma.CoffeeName} - Price/Fiyat: {ma.Price}").ToList()));
            CreateMap<Order, GetOrderByIDModel>() //getByID
                   .ForMember(dest => dest.customers, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname + " -- email :" + src.Customer.Email))
               .ForMember(dest => dest.Coffees, opt => opt.MapFrom(src => src.Coffees.Select(ma => $"{ma.CoffeeName} - Price/Fiyat: {ma.Price}").ToList()));

        }
    }
}
