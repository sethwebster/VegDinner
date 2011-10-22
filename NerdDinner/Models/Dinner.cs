using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace NerdDinner.Models
{
    public class Dinner
    {
        [HiddenInput(DisplayValue = false)]
        public int DinnerID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title may not be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Date is required")]
        [Display(Name = "Event Date")]
        [HiddenInput(DisplayValue = false)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "The date is required")]
        [NotMapped]
        [UIHint("Date")]
        [Display(Name = "Date of your event")]
        public DateTime TheDate
        {
            get
            {
                return EventDate.Date;
            }
            set
            {
                EventDate = DateTime.Parse(value.Date.ToShortDateString() + " " + EventDate.ToShortTimeString());
            }
        }

        [Required(ErrorMessage = "The time is required")]
        [NotMapped]
        [Display(Name = "What time should guests arrive?")]
        [UIHint("Time")]
        public TimeSpan TheTime
        {
            get
            {
                return EventDate.TimeOfDay;
            }
            set
            {
                EventDate = DateTime.Parse(EventDate.ToShortDateString() + " " + value.Hours + ":" + value.Minutes);
            }
        }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(256, ErrorMessage = "Description may not be longer than 256 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(20, ErrorMessage = "Hosted By name may not be longer than 20 characters")]
        [Display(Name = "Host's Name")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Contact info is required")]
        [StringLength(20, ErrorMessage = "Contact info may not be longer than 20 characters")]
        [Display(Name = "Contact Info")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, ErrorMessage = "Address may not be longer than 50 characters")]
        [Display(Name = "Address, City, State, ZIP")]
        public string Address { get; set; }

        [UIHint("CountryDropDown")]
        public string Country { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double Latitude { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double Longitude { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string HostedById { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }

        [Required(ErrorMessage = "Please select the types of cuisine which will be served")]
        [Display(Name = "Cuisine Types")]
        public string CuisineTypes { get; set; }

        public bool IsHostedBy(string userName)
        {
            return String.Equals(HostedById ?? HostedBy, userName, StringComparison.Ordinal);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeNameId == userName || (r.AttendeeNameId == null && r.AttendeeName == userName));
        }

        [UIHint("LocationDetail")]
        [NotMapped]
        public LocationDetail Location
        {
            get
            {
                return new LocationDetail() { Latitude = this.Latitude, Longitude = this.Longitude, Title = this.Title, Address = this.Address };
            }
            set
            {
                this.Latitude = value.Latitude;
                this.Longitude = value.Longitude;
                this.Title = value.Title;
                this.Address = value.Address;
            }
        }
    }

    public class LocationDetail
    {
        public double Latitude;
        public double Longitude;
        public string Title;
        public string Address;
    }
}