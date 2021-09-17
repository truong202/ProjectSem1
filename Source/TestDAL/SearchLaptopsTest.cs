using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;


namespace TestDAL
{
    public class SearchLaptopTest
    {
        private static Category[] Categories =
          new[] { new Category { CategoryId = 1, CategoryName = "Gaming" },
                  new Category { CategoryId = 2, CategoryName = "Office" },
                  new Category { CategoryId = 3, CategoryName = "Multimedia" },
                  new Category { CategoryId = 4, CategoryName = "Workstation" }};
        private static Manufactory[] Manufactories =
            new[] { new Manufactory{ManufactoryId = 1, ManufactoryName = "ASUS", Website="https://www.asus.com/vn/",
                        Address="117-119-121 Nguyen Du, Ben Thanh Ward, Distric 1, Ho Chi Minh City" },
                    new Manufactory{ManufactoryId = 2, ManufactoryName = "MSI", Website="https://vn.msi.com/",
                        Address="Zoom L802, 8th Floor, 99 Nguyen Thi Minh Khai - Ben Thanh Ward - Distric 1 - Ho Chi Minh City" },
                    new Manufactory{ManufactoryId = 3, ManufactoryName = "Acer", Website="https://www.acervietnam.com.vn/",
                        Address="1st Floor, Dao Duy Anh building, 9A Dao Duy Anh, Phuong Lien Ward, Dong Da Distric, Hanoi" },
                    new Manufactory{ManufactoryId = 4, ManufactoryName = "Apple", Website="https://www.apple.com/vn/",
                        Address="Zoom 901, Deutsches Haus Ho Chi Minh City, 33 Le Duan Street, Ben Nghe Ward, Distric 1, Ho Chi Minh City" },
                    new Manufactory{ManufactoryId = 5, ManufactoryName = "LG", Website="https://www.lg.com/vn",
                        Address="industrial lot 2, Trang Due Industrial Park, Le Loi commune, An Duong Distric, Hai Phong City" },
                    new Manufactory{ManufactoryId = 6, ManufactoryName = "Lenovo", Website="https://www.lenovo.com/vn/vn/",
                        Address="Zoom 709A, 7th Floor, Oriental Tower, 324 Tay Son Street, Nga Tu So, Dong Da Distric, Hanoi" },
                    new Manufactory{ManufactoryId = 7, ManufactoryName = "Dell", Website="https://www.dell.com/vn/p/",
                        Address="P1402A,14th Floor, IPH Indochina Plaza HN, 241 Xuan Thuy, Dich Vong Hau Ward, Cau Giay Distric, Hanoi" }};

        private static Laptop laptop1 = new Laptop
        {
            LaptopId = 5,
            LaptopName = "ASUS D515DA EJ711T",
            CategoryInfo = Categories[1],
            ManufactoryInfo = Manufactories[0],
            CPU = "AMD Ryzen 3-3250U",
            Ram = "4GB DDR4 on board",
            HardDrive = "512GB M.2 NVMe PCIe 3.0 SSD",
            VGA = "AMD Radeon Graphics",
            Display = @"15.6-inch, FHD (1920 x 1080) 16:9, 200nits, Screen-to-body ratio: 83%",
            Battery = "2-cell 37WHrs",
            Weight = "1.80 kg",
            Materials = "Plastic",
            Ports = @"1x USB 3.2 Gen 1 Type-A, 1x USB 3.2 Gen 1 Type-C, 2x USB 2.0 Type-A, 1x HDMI 1.4, 1x 3.5mm Combo Audio Jack",
            NetworkAndConnection = "LAN, Wi-Fi 5(802.11ac), Bluetooth v4.2",
            Security = "PIN",
            Keyboard = "No led",
            Audio = "No infor",
            Size = "36.00 x 23.50 x 1.99 cm",
            OS = "Windows 10 Home",
            Quantity = 24
            ,
            Price = 11990000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop2 = new Laptop
        {
            LaptopId = 13,
            LaptopName = "Acer Aspire 3 A315 56 37DV",
            CategoryInfo = Categories[1],
            ManufactoryInfo = Manufactories[2],
            CPU = "Intel Core i3-1005G1",
            Ram = "4GB DDR4 2400MHz Onboard",
            HardDrive = "256GB SSD M.2 PCIE",
            VGA = "Intel UHD Graphics",
            Display = @"15.6"" FHD (1920 x 1080) Acer ComfyView LCD, Anti-Glare",
            Battery = "4 Cell 59WHr",
            Weight = "1.7 kg",
            Materials = "Plastic",
            Ports = @"1x USB 3.1, 2x USB 2.0, HDMI, RJ-45",
            NetworkAndConnection = "LAN, 802.11 ac, Bluetooth v4.2",
            Security = "PIN",
            Keyboard = "No led",
            Audio = "Realtek High Definition",
            Size = "363 x 247.5 x 19.9 (mm)",
            OS = "Windows 10 Home",
            Quantity = 7,
            Price = 10990000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop3 = new Laptop
        {
            LaptopId = 18,
            LaptopName = "Lenovo IdeaPad Slim 3 15IIL05",
            CategoryInfo = Categories[1],
            ManufactoryInfo = Manufactories[5],
            CPU = "Intel Core i3 1005G1",
            Ram = "4GB DDR4 2666Mhz",
            HardDrive = "512GB SSD PCIE",
            VGA = "Intel UHD Graphics",
            Display = @"15.6"" FHD (1920*1080), TN panel, Anti-Glare",
            Battery = "35Whrs",
            Weight = "1.85 kg",
            Materials = "Plastic",
            Ports = @"2x USB 3.2, 1x USB 2.0, HDMI, 3.5mm audio jack, 1x Micro SD",
            NetworkAndConnection = "Wi-Fi 802.11 a/b/g/n/ac, Bluetooth v5.0",
            Security = "PIN",
            Keyboard = "No Led",
            Audio = "Stereo speakers",
            Size = "362.2 mm x 253.4 mm x 19.9 mm",
            OS = "Windows 10 Home",
            Quantity = 10,
            Price = 12690000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop4 = new Laptop
        {
            LaptopId = 24,
            LaptopName = "Dell Latitude 3520 (70251603)",
            CategoryInfo = Categories[1],
            ManufactoryInfo = Manufactories[6],
            CPU = "Intel Core i3 1115G4",
            Ram = "4GB DDR4 3200Mhz",
            HardDrive = "256GB PCIe NVMe SSD",
            VGA = "Intel UHD Graphics",
            Display = @"15.6 inch HD (1366 x 768) WVA Anti-glare 60Hz",
            Battery = "3 cell 41WHrs",
            Weight = "1.79 kg",
            Materials = "Plastic",
            Ports = @"Audio jack , 1 x USB 2.0, 1 x  USB 3.2 Gen 1 Type-A port 1 x RJ-45, 1 x HDMI 1.4 , 1 x USB 3.2 Gen 1 Type-A port PowerShare, 1 x USB 3.2 Gen2x2 Type-C port",
            NetworkAndConnection = "LAN, Intel Wi-Fi 6 AX201, 2 X 2, 802.11ax with Bluetooth 5.1",
            Security = "PIN",
            Keyboard = "No led",
            Audio = "2x 2W Stereo speaker",
            Size = "326 x 226 x 18.6mm",
            OS = "Fedora",
            Quantity = 5,
            Price = 14439000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop5 = new Laptop
        {
            LaptopId = 26,
            LaptopName = "Macbook Pro 13 Touchbar Z11D000E5",
            CategoryInfo = Categories[2],
            ManufactoryInfo = Manufactories[3],
            CPU = "Apple M1",
            Ram = "16GB",
            HardDrive = "256GB PCIe NVMe SSD",
            VGA = "8 core GPU",
            Display = @"Retina 13.3 inch (2560x1600) IPS Led Backlit True Tone",
            Battery = "58.2WHrs",
            Weight = "1.4 kg",
            Materials = "Metal",
            Ports = @"USB 3.1 Gen2, 2x Thunder Bolt 3",
            NetworkAndConnection = "Wifi 802.11ac - Bluetooth 5.0",
            Security = "PIN, Touch ID",
            Keyboard = "No led",
            Audio = "Stereo speakers",
            Size = "304.1 x 212.4 x 15.6 mm",
            OS = "MAC OS",
            Quantity = 20,
            Price = 38989000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop6 = new Laptop
        {
            LaptopId = 27,
            LaptopName = "Macbook Air 13 MVFM2",
            CategoryInfo = Categories[2],
            ManufactoryInfo = Manufactories[3],
            CPU = "Intel Core i5 8th",
            Ram = "8GB LPDDR3",
            HardDrive = "128 PCIe NVMe SSD",
            VGA = "Intel UHD 617",
            Display = @"Retina 13.3 inch (2560x1600) IPS Led Backlit True Tone",
            Battery = "49.9WHrs",
            Weight = "1.25 kg",
            Materials = "Metal",
            Ports = @"USB 3.1 Gen2, 2x Thunder Bolt 3",
            NetworkAndConnection = "Wifi 802.11ac - Bluetooth 4.2",
            Security = "PIN, Touch ID",
            Keyboard = "No led",
            Audio = "Stereo speakers",
            Size = "304.1 x 212.4 x 4.1 mm",
            OS = "MAC OS",
            Quantity = 20,
            Price = 27259000,
            WarrantyPeriod = "12 month"
        };

        private static Laptop laptop7 = new Laptop
        {
            LaptopId = 25,
            LaptopName = "LG Gram 14ZD90P-G.AX51A5",
            CategoryInfo = Categories[2],
            ManufactoryInfo = Manufactories[4],
            CPU = "Intel Core i5-1135G7",
            Ram = "8GB LPDDR4X 4266MHz",
            HardDrive = "256GB PCIe NVMe SSD",
            VGA = "Intel Iris Xe Graphics",
            Display = @"14.0 inch (30.2cm) WUXGA (1920*1200) IPS LCD",
            Battery = "72WHrs",
            Weight = "0.999 kg",
            Materials = "Metal",
            Ports = @"HP-Out(4Pole Headset, US type), USB 3.2 Gen2x1 (x2), HDMI,  USB 4 Gen3x2 Type C (x2, with Power Delivery, Display Port, Thunderbolt 4)",
            NetworkAndConnection = "Intel Wi-Fi 6 AX201D2W; Bluetooth 5.0",
            Security = "PIN, Fingerprint",
            Keyboard = "No led",
            Audio = "DTS: X Ultra",
            Size = "313.4 x 215.2 x 16.8 mm",
            OS = "No OS",
            Quantity = 5,
            Price = 14439000,
            WarrantyPeriod = "12 month"
        };
        private static List<Laptop> listOffice = new List<Laptop>() { laptop1, laptop2, laptop3, laptop4 };
        private static List<Laptop> listMacbook = new List<Laptop>() { laptop5, laptop6 };
        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                return new[]
                {
                    new object[] {"ASUS D515DA EJ711T", new List<Laptop>(){laptop1}},
                    new object[] {"office", listOffice},
                    new object[] {"macbook", listMacbook},
                    new object[] {"LG", new List<Laptop>(){laptop7}},
                    new object[] {"dfkaskfjksd", null}
                };
            }
        }

        LaptopDAL laptopDAL = new LaptopDAL();

        [Theory, MemberData(nameof(SplitCountData))]
        public void SearchLaptopsTest1(string searchValue, List<Laptop> expected)
        {
            List<Laptop> result = laptopDAL.Search(searchValue, 0);
                Assert.Equal(result, expected);
        }
    }
}