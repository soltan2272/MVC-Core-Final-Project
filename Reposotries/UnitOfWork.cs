using Data;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Models;
using Models.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposotries
{
    public class UnitOfWork : IUnitOfWork
    {
        Project_Context Context;
        

     //   IGenericRepostory<User> UserRepo;
        IGenericRepostory<Category> CategoryRepo;
      //  IGenericRepostory<Contact> ContactRepo;
        IGenericRepostory<Courier> CourierRepo;
        IGenericRepostory<Feedback> FeedbackRepo;
        IGenericRepostory<Offer> OfferRepo;
        IGenericRepostory<Order> OrderRepo;
        IGenericRepostory<Payment> PaymentRepo;
        IGenericRepostory<Product> ProductRepo;
        IGenericRepostory<Store> StoreRepo;
        IGenericRepostory<Images> ImagesRepo;
        IGenericRepostory<ContactUs> ContactRepo;

        public UnitOfWork(Project_Context context,
                            IGenericRepostory<Category> categoryRepo,// IGenericRepostory<Contact> contactRepo,
                            IGenericRepostory<Courier> courierRepo, IGenericRepostory<Feedback> feedbackRepo,
                            IGenericRepostory<Offer> offerRepo, IGenericRepostory<Order> orderRepo,
                            IGenericRepostory<Payment> paymentRepo, IGenericRepostory<Product> productRepo,
                            IGenericRepostory<Store> storeRepo, IGenericRepostory<Images> imagesRepo,
                            IGenericRepostory<ContactUs> contactRepo
)
        {
            Context = context;
            CategoryRepo = categoryRepo;
           // ContactRepo = contactRepo;
            CourierRepo = courierRepo;
            FeedbackRepo = feedbackRepo;
            OfferRepo = offerRepo;
            OrderRepo = orderRepo;
            PaymentRepo = paymentRepo;
            ProductRepo = productRepo;
            StoreRepo = storeRepo;
            ImagesRepo = imagesRepo;
            //UserRepo = userRepo;
            ContactRepo = contactRepo;
        }

        public IGenericRepostory<Category> GetCategoryRepo()
        {
            return CategoryRepo;

        }
     
        public IGenericRepostory<Courier> GetCourierRepo()
        {
            return CourierRepo;

        }
        public IGenericRepostory<Feedback> GetFeedbackRepo()
        {
            return FeedbackRepo;

        }

        

        public IGenericRepostory<Offer> GetOfferRepo()
        {
            return OfferRepo;

        }
        public IGenericRepostory<Order> GetOrderRepo()
        {
            return OrderRepo;

        }
        public IGenericRepostory<Payment> GetPaymentRepo()
        {
            return PaymentRepo;

        }
        public IGenericRepostory<Product> GetProductRepo()
        {
            return ProductRepo;

        }
        public IGenericRepostory<Images> GetImagesRepo()
        {
            return ImagesRepo;
        }
        public IGenericRepostory<Store> GetStoreRepo()
        {
            return StoreRepo;

        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }

        public IGenericRepostory<ContactUs> GetContactRepo()
        {
            return ContactRepo;
        }
        public Project_Context context()
        {
            return Context;
        }
    }
}
