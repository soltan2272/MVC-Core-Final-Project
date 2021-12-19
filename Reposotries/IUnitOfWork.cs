using Data;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposotries
{
    public interface IUnitOfWork
    {
       // IGenericRepostory<Admin> GetAdminRepo();
        IGenericRepostory<Category> GetCategoryRepo();
       // IGenericRepostory<Contact> GetContactRepo();
        IGenericRepostory<Courier> GetCourierRepo();
        IGenericRepostory<Feedback> GetFeedbackRepo();
        IGenericRepostory<Offer> GetOfferRepo();
        IGenericRepostory<Order> GetOrderRepo();
        IGenericRepostory<Payment> GetPaymentRepo();
        IGenericRepostory<Product> GetProductRepo();
        IGenericRepostory<Store> GetStoreRepo();
        IGenericRepostory<Images> GetImagesRepo();
        IGenericRepostory<ContactUs> GetContactRepo();
        //  IGenericRepostory<Supplier> GetSupplierRepo();
        Project_Context context();

        Task Save();

    }
}
