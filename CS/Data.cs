using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXSample {
    public class DataSource {
        public Contact CurrentItem { get; set; }
        public DataSource() {
            CurrentItem = new Contact {
                FirstName = "Carolyn",
                LastName = "Baker",
                Phone = "(555)349-3010",
                Address = "1198 Theresa Cir",
                City = "Whitinsville",
                State = "MA",
                Zip = "01582",
            };
            CurrentItem.Photo = GetPhoto(CurrentItem);
        }
        byte[] GetPhoto(Contact contact) {
            return GetPhoto(contact.FirstName + contact.LastName + ".jpg");
        }
        byte[] GetPhoto(string name) {
            return File.ReadAllBytes(@"Images\" + name);
        }
    }
    [MetadataType(typeof(ContactMetadata))]
    public class Contact {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public object Photo { get; set; }
    }
    public class ContactMetadata {
        public static void BuildMetadata(MetadataBuilder<Contact> builder) {
            builder.DataFormLayout()
                .Group("<>")
                    .ContainsProperty(x => x.Photo)
                .EndGroup()
                .GroupBox("General")
                    .ContainsProperty(x => x.FirstName)
                    .ContainsProperty(x => x.LastName)
                .EndGroup()
                .TabbedGroup("Info")
                    .Group("Contacts")
                        .ContainsProperty(x => x.Phone)
                        .ContainsProperty(x => x.Email)
                    .EndGroup()
                    .Group("Address")
                        .ContainsProperty(x => x.State)
                        .ContainsProperty(x => x.City)
                        .ContainsProperty(x => x.Address)
                        .ContainsProperty(x => x.Zip)
                    .EndGroup()
                .EndGroup();
            builder.Property(x => x.Photo).LayoutControlEditor("ImageTemplate").DisplayName(string.Empty);
            builder.Property(x => x.Email).NullDisplayText("<empty>");
            builder.Property(x => x.City).RegExMask(@"\w{1,25}");
        }
    }
}
