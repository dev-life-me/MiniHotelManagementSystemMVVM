using DAL.Entities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ManageBookingDetailService
    {
        private readonly BookingDetailRepository _detailRepo;
        public ManageBookingDetailService(BookingDetailRepository detailRepo)
        {
            _detailRepo = detailRepo;
        }

        public List<BookingDetail> GetAll()
        {
            return _detailRepo.GetAll().ToList();
        }

        public List<BookingDetail> GetByReservationId(int reservationId)
        {
            return _detailRepo.GetAll().Where(d => d.BookingReservationId == reservationId).ToList();
        }

        public void Add(BookingDetail detail)
        {
            _detailRepo.Add(detail);
            
        }

        public void Update(BookingDetail detail)
        {
            _detailRepo.Update(detail);
        }

        public void Remove(BookingDetail detail)
        {
            _detailRepo.Remove(detail);
        }
    }
} 