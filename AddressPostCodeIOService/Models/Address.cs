using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressPostcodeIOService.Models
{
    public class Address
    {
        [Key]
        [Column("AP_Id")]
        public int Id { get; set; }

        [Column("AP_BuildingNumber")]
        public string BuildingNumber { get; set; }

        [Column("AP_BuildingName")]
        public string BuildingName { get; set; }

        [Column("AP_SubBuildingName")]
        public string SubBuildingName { get; set; }

        [Column("AP_AddressLine1")]
        public string AddressLine1 { get; set; }

        [Column("AP_AddressLine2")]
        public string AddressLine2 { get; set; }

        [Column("AP_AddressLine3")]
        public string AddressLine3 { get; set; }

        [Column("AP_PostTown")]
        public string PostTown { get; set; }

        [Column("AP_PostalCounty")]
        public string PostalCounty { get; set; }

        [Column("AP_County")]
        public string County { get; set; }

        [Column("AP_District")]
        public string District { get; set; }

        [Column("AP_Ward")]
        public string Ward { get; set; }

        [Column("AP_Country")]
        public string Country { get; set; }

        [Column("AP_Postcode")]
        [Required]
        [MaxLength(30)]
        public string Postcode { get; set; }

        [Column("AP_PostcodeInward")]
        [MaxLength(30)]
        public string PostcodeInward { get; set; }

        [Column("AP_PostcodeOutward")]
        [MaxLength(30)]
        public string PostcodeOutward { get; set; }

        [Column("AP_Latitude")]
        public float Latitude { get; set; }

        [Column("AP_Longitude")]
        public float Longitude { get; set; }

        [Column("AP_UpdatedBy")]
        public int AP_UpdatedBy { get; set; }

        [Column("AP_UpdateDate")]
        public DateTime UpdateDate { get; set; }

        [Column("AP_CreatedBy")]
        public int CreatedBy { get; set; }

        [Column("AP_CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column("AP_MigrationRef")]
        public int MigrationRef { get; set; }

        [Column("AP_Deleted")]
        public bool Deleted { get; set; }
    }
}
