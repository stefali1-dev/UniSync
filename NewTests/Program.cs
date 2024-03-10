//// See https://aka.ms/new-console-template for more information
//using GlobalBuyTicket.Domain.Entities;
//using Infrastructure;
//using Infrastructure.Repositories;

//Console.WriteLine("Hello, World!");

//Category category = Category.Create("Music").Value;
//GlobalBuyTiketContext context = new();
//CategoryRepository repository = new (context);
//await repository.AddAsync(category);
//var result = await repository.FindByIdAsync(category.CategoryId);
//Console.WriteLine(result.Value.CategoryId);