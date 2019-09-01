using System;
using System.Collections.Generic;
using System.Text;
using FINANCE.CORE.Models;
using FINANCE.INFRA.Infrastructure;
using FINANCE.INFRA.Repositories;


namespace FINANCE.SERVICE.Core
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IUBorrowerRepository uBorrowerRepository;
        private readonly IUnitOfWork unitOfWork;

        public BorrowerService(IUBorrowerRepository uBorrowerRepository, IUnitOfWork unitOfWork)
        {
            this.uBorrowerRepository = uBorrowerRepository;
            this.unitOfWork = unitOfWork;

        }

        public void CreateBorrower(Borrower borrower)
        {
            uBorrowerRepository.Create(borrower);
            SaveBorrower();
        }

        public void Edit(Borrower borrower)
        {
            uBorrowerRepository.Edit(borrower);
            SaveBorrower();
        }

        public IEnumerable<Borrower> GetAllUsers()
        {
            return uBorrowerRepository.GetAll();
        }
        public Borrower GetBorrowerByBorrowerID(int ID)
        {
            return uBorrowerRepository.GetBorrowerByBorrowerID(ID);
        }
        public void Delete(Borrower borrower)
        {
            uBorrowerRepository.Delete(borrower);
            SaveBorrower();
        }
        public void SaveBorrower()
        {
            unitOfWork.Commit();
        }
    }

}
    public interface IBorrowerService
    {
    void Delete(Borrower borrower);
    void Edit(Borrower borrower);
    Borrower GetBorrowerByBorrowerID(int ID);
    IEnumerable<Borrower> GetAllUsers();
    void CreateBorrower(Borrower borrower);
    }
