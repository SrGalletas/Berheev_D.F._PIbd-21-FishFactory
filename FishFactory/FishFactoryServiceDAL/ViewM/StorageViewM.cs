using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFactoryServiceDAL.ViewM
{
    public class StorageViewM
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        public List<StorageFishViewM> StorageFishes { get; set; }
    }
}
