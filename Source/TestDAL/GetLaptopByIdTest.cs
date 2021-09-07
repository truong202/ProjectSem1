using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;


namespace TestDAL
{
    public class GetLaptopByIdTest
    {
        private LaptopDAL laptopDAL = new LaptopDAL();
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
            LaptopId = 1,
            LaptopName = "ASUS TUF Gaming F15 FX506HC HN002T",
            CategoryInfo = Categories[0],
            ManufactoryInfo = Manufactories[0],
            CPU = "Intel Core I5-11400H",
            Ram = "8GB DDR4 2933MHz",
            HardDrive = "512GB SSD M.2 PCIE G3X2",
            VGA = "NVIDIA GeForce RTX 3050 Laptop",
            Display = @"15.6"" FHD (1920 x 1080) IPS, 144Hz, Wide View, 250nits, Narrow Bezel, Non-Glare with 45% NTSC, 62.5% sRGB",
            Battery = "48WHrs",
            Weight = "2.3kg",
            Materials = "Plastic",
            Ports = @"2x Type C USB 3.2 Gen 2 with Power Delivery and Display Port, 2x USB 3.2 Gen 2 Type-A, 1x HDMI 2.0b, 1x 3.5mm  Combo Audio Jack, 1x RJ45 LAN",
            NetworkAndConnection = "LAN, Wifi 6 802.11AC (2X2), Bluetooth 5.0",
            Security = "PIN code",
            Keyboard = "1-Zone RGB",
            Audio = "DTS:X Ultra",
            Size = "35.9 x 25.6 x 2.28 ~ 2.43 cm",
            OS = "Windows 10 Home",
            Quantity = 15,
            Price = 24990000,
            WarrantyPeriod = "24 month"
        };
        private static Laptop laptop2 = new Laptop
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
            Quantity = 24,
            Price = 11990000,
            WarrantyPeriod = "12 month"
        };
        private static Laptop laptop3 = new Laptop
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
            Size = "304.1 x 212.4 x 4.1  mm",
            OS = "MAC OS",
            Quantity = 20,
            Price = 27259000,
            WarrantyPeriod = "12 month"
        };
        private Category a = new Category();
        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                return new[]
                {
                    new object[] {1, laptop1},
                    new object[] {5, laptop2},
                    new object[] {27, laptop3},
                    new object[] {0, null},
                    new object[] {29, null}
                };
            }
        }
        // [Theory]
        // [InlineData(1)]
        // [InlineData(5)]
        [Theory, MemberData(nameof(SplitCountData))]
        public void GetLaptopByIdTest1(int laptopId, Laptop expected)
        {
            Laptop result = laptopDAL.GetById(laptopId);
            Assert.Equal(result, expected);
        }

        // [Theory]
        // [InlineData(1000)]
        // [InlineData(0)]
        // public void Test2(int laptopId)
        // {
        //     Laptop result = laptopDAL.GetById(laptopId);
        //     Assert.True(result == null);
        // }
    }
}