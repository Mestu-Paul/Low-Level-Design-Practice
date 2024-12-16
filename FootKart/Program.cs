// See https://aka.ms/new-console-template for more information
using FootKart.Services;

var footKart = FoodKartService.GetInstance();
footKart.Run();