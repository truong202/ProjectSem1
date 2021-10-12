DROP DATABASE IF EXISTS laptop_store;
CREATE DATABASE IF NOT EXISTS laptop_store;

USE laptop_store;

CREATE TABLE staffs (
    staff_id INT AUTO_INCREMENT,
    staff_name VARCHAR(50) NOT NULL,
    username VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role INT NOT NULL,
    PRIMARY KEY (staff_id)
);


CREATE TABLE customers (
    customer_id INT AUTO_INCREMENT,
    customer_name VARCHAR(50),
    address VARCHAR(100),
    phone VARCHAR(10) NOT NULL UNIQUE,
    PRIMARY KEY (customer_id)
);

CREATE TABLE categories (
    category_id INT AUTO_INCREMENT,
    category_name VARCHAR(50) NOT NULL UNIQUE,
    PRIMARY KEY (category_id)
);

CREATE TABLE manufactories (
    manufactory_id INT AUTO_INCREMENT,
    manufactory_name VARCHAR(50) NOT NULL UNIQUE,
    website VARCHAR(255),
    address VARCHAR(255),
    PRIMARY KEY (manufactory_id)
);

CREATE TABLE laptops (
    laptop_id INT AUTO_INCREMENT,
    laptop_name VARCHAR(100) NOT NULL,
    category_id INT NOT NULL,
    manufactory_id INT NOT NULL,
    CPU VARCHAR(100) NOT NULL,
    Ram VARCHAR(100) NOT NULL,
    hard_drive VARCHAR(100) NOT NULL,
    VGA VARCHAR(100) NOT NULL,
    display VARCHAR(255) NOT NULL,
    battery VARCHAR(100) NOT NULL,
    weight FLOAT NOT NULL,
    materials VARCHAR(100) NOT NULL,
    ports VARCHAR(255) NOT NULL,
    network_and_connection VARCHAR(100) NOT NULL,
    security VARCHAR(100) NOT NULL,
    keyboard VARCHAR(100) NOT NULL,
    audio VARCHAR(100) NOT NULL,
    size VARCHAR(100) NOT NULL,
    warranty_period VARCHAR(100) NOT NULL,
    OS VARCHAR(100) NOT NULL,
    price DECIMAL NOT NULL DEFAULT 0,
    quantity INT NOT NULL DEFAULT 0,
    PRIMARY KEY (laptop_id),
    FOREIGN KEY (category_id)
        REFERENCES categories (category_id),
    FOREIGN KEY (manufactory_id)
        REFERENCES manufactories (manufactory_id)
);


CREATE TABLE orders (
    order_id INT AUTO_INCREMENT,
    customer_id INT NOT NULL,
    seller_id INT NOT NULL,
    accountant_id INT,
    order_date DATETIME DEFAULT NOW(),
    order_status INT NOT NULL,
    PRIMARY KEY (order_id),
    FOREIGN KEY (customer_id)
        REFERENCES customers (customer_id),
    FOREIGN KEY (seller_id)
        REFERENCES staffs (staff_id),
    FOREIGN KEY (accountant_id)
        REFERENCES staffs (staff_id)
);

CREATE TABLE order_details (
    order_id INT,
    laptop_id INT,
    unit_price DECIMAL,
    quantity INT NOT NULL DEFAULT 1,
    PRIMARY KEY (order_id , laptop_id),
    FOREIGN KEY (order_id)
        REFERENCES orders (order_id),
    FOREIGN KEY (laptop_id)
        REFERENCES laptops (laptop_id)
);

 -- pass 12345678 
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn A', 'seller001', '25d55ad283aa400af464c76d713c07ad', 1);
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn B', 'seller002', '25d55ad283aa400af464c76d713c07ad', 1);
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn C', 'seller003', '25d55ad283aa400af464c76d713c07ad', 1);
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn D', 'accountant001', '25d55ad283aa400af464c76d713c07ad', 2);
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn E', 'accountant002', '25d55ad283aa400af464c76d713c07ad', 2);
INSERT INTO staffs(staff_name, username, password, role)
VALUES ('Nguyễn Văn F', 'accountant003', '25d55ad283aa400af464c76d713c07ad', 2);

INSERT INTO customers(customer_name, address, phone)
VALUES ('Phạm Công Hưng', 'Nam Định', '0904844014');
INSERT INTO customers(customer_name, address, phone)
VALUES ('Phạm Công Hà', 'Nam Định', '0906450904');
INSERT INTO customers(customer_name, phone, address)
VALUES ('Customer1', '0836984311', 'Ho Chi Minh');
INSERT INTO customers(customer_name, phone, address)
VALUES ('Customer2', '0836984312', 'Bac Giang');
INSERT INTO customers(customer_name, phone, address)
VALUES ('Customer3', '0836984313', 'Ha Noi');

INSERT INTO categories(category_name)
VALUES ('Gaming'), ('Office'), ('Multimedia'), ('Workstation');

INSERT INTO manufactories(manufactory_name, website, address)
VALUES ('ASUS', 'https://www.asus.com/vn/', '117-119-121 Nguyen Du, Ben Thanh Ward, Distric 1, Ho Chi Minh City'),
	   ('MSI', 'https://vn.msi.com/', 'Zoom L802, 8th Floor, 99 Nguyen Thi Minh Khai - Ben Thanh Ward - Distric 1 - Ho Chi Minh City'),
	   ('Acer', 'https://www.acervietnam.com.vn/', '1st Floor, Dao Duy Anh building, 9A Dao Duy Anh, Phuong Lien Ward, Dong Da Distric, Hanoi'), 
	   ('Apple', 'https://www.apple.com/vn/', 'Zoom 901, Deutsches Haus Ho Chi Minh City, 33 Le Duan Street, Ben Nghe Ward, Distric 1, Ho Chi Minh City'), 
	   ('LG', 'https://www.lg.com/vn', 'industrial lot 2, Trang Due Industrial Park, Le Loi commune, An Duong Distric, Hai Phong City'),
	   ('Lenovo', 'https://www.lenovo.com/vn/vn/', 'Zoom 709A, 7th Floor, Oriental Tower, 324 Tay Son Street, Nga Tu So, Dong Da Distric, Hanoi'),
       ('Dell', 'https://www.dell.com/vn/p/', 'P1402A,14th Floor, IPH Indochina Plaza HN, 241 Xuan Thuy, Dich Vong Hau Ward, Cau Giay Distric, Hanoi');

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('ASUS TUF Gaming F15 FX506HC HN002T', 1, 1, 'Intel Core I5-11400H', '8GB DDR4 2933MHz', '512GB SSD M.2 PCIE G3X2', 'NVIDIA GeForce RTX 3050 Laptop',
'15.6" FHD (1920 x 1080) IPS, 144Hz, Wide View, 250nits, Narrow Bezel, Non-Glare with 45% NTSC, 62.5% sRGB', '48WHrs', 2.3, 'Plastic',
'2x Type C USB 3.2 Gen 2 with Power Delivery and Display Port, 2x USB 3.2 Gen 2 Type-A, 1x HDMI 2.0b, 1x 3.5mm  Combo Audio Jack, 1x RJ45 LAN',
'LAN, Wifi 6 802.11AC (2X2), Bluetooth 5.0', 'PIN code', '1-Zone RGB','DTS:X Ultra','35.9 x 25.6 x 2.28 ~ 2.43 cm', '24 month', 'Windows 10 Home', 24990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Asus TUF Dash FX516PE HN005T', 1, 1, 'Intel Core i7-11370H', '8GB Onboard DDR4 3200MHz', '512GB SSD M.2 NVMe', 'NVIDIA GeForce  RTX 3050Ti 4GB', 
'15.6" FHD (1920 x 1080) 16:9, Anti-Glare Display, 62.5% sRGB, 144Hz, IPS, Adaptive-Sync', '4 Cell 76Whr', 2, 'Plastic',
'1x Type C USB 4 with Power Delivery, Display Port and Thunderbolt 4, 3x USB 3.2 Gen 1 Type-A, 1x HDMI 2.0b, 1x RJ45, 1x 3.5mm Combo Audio Jack',
'LAN, 802.11ax (2X2), Bluetooth 5.1', 'PIN code', 'Backlit Chiclet Keyboard Blue', 'DTS:X Ultra', '36.0 x 25.2 x 1.99 cm', '24 month', 'Windows 10 Home', 27990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, Keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Asus ROG Strix G15 G513QC HN015T', 1, 1, 'AMD Ryzen R7-5800H', '8GB DDR4 3200MHz', '512GB SSD M.2 NVMe', 'NVIDIA GeForce RTX 3050 4GB',
'15.6 inch FHD (1920 x 1080)IPS level. Non-Glare, 144Hz AdaptiveSync, Nanoedge', '4 Cell 56WHrs', 2.3, 'Plastic',
'1x Type C USB 3.2 Gen 1 with Power Delivery and Display Port, 3x USB 3.2 Gen 1 Type-A, 1x 3.5mm Combo Audio Jack, 1x HDMI 2.0b',
'Intel 802.11ax (2x2) Wi-Fi 6 (Gig+), Bluetooth v5.1', 'PIN', '4 Zone RGB', 'Smart AMP technology', '35.4(W) x 25.9(D) x 2.07 ~ 2.6(H) cm',
'24 month', 'Windows 10 Home', 27990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Asus ROG Zephyrus G14 Alan Walker Edition GA401QEC K2064T', 1, 1, 'AMD Ryzen 9-5900HS', '16GB(8GB DDR4 on board + 8GB DDR4-3200Mhz SO-DIMM)', 
'1TB M.2 NVMe PCIe 3.0 SSD', 'NVIDIA GeForce RTX 3050 Ti', '14.0 inch QHD (2560 x 1440) 16:9, IPS 120Hz, 100% anti-glare display', '4 Cell 76WHr',
1.7, 'Plastic and Metal', '1x USB 3.2 Gen 2 Type-C, 2x USB 3.2 Gen 1 Type-A, 1x USB 3.2 Gen 2 Type-C support DisplayPort / power delivery / G-SYNC',
'LAN, Intel Wi-Fi 6 with Gig+ performance (802.11AX), Bluetooth v5.0', 'PIN, Fingerprint', 'White led', '2x 2.5W speaker with smart AMP technology, Dolby Atmos Software', 
'32.4 x 22.0 x 1.99 ~ 1.99 cm', '24 month', 'Windows 10 Home', 49990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('ASUS D515DA EJ711T', 1, 2, 'AMD Ryzen 3-3250U', '4GB DDR4 on board', '512GB M.2 NVMe PCIe 3.0 SSD', 'AMD Radeon Graphics',
'15.6-inch, FHD (1920 x 1080) 16:9, 200nits, Screen-to-body ratio: 83%', '2-cell 37WHrs', 1.8, 'Plastic',
'1x USB 3.2 Gen 1 Type-A, 1x USB 3.2 Gen 1 Type-C, 2x USB 2.0 Type-A, 1x HDMI 1.4, 1x 3.5mm Combo Audio Jack','LAN, Wi-Fi 5(802.11ac), Bluetooth v4.2', 'PIN',
 'No led', 'No infor', '36.00 x 23.50 x 1.99 cm', '12 month', 'Windows 10 Home', 11990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('ProArt StudioBook One W590G6T', 1, 4, 'Intel Core i9-9980HK', '64GB DDR4 2666MHz', '1TB M.2 NVMe PCIe 3.0 SSD', 'NVIDIA Quadro RTX 6000',
'15,6" LED UHD (3840 x 2160) 1000:1, anti-glare, 100% sRGB, PANTONE Validated, Gorilla Glass 5', '12-cell 90WHrs', 2.9, 'Metal',
'3 x USB-C Thunderbolt 3 support DisplayPort 1.4 (40Gbps)','Wifi 2×2 802.11ac, Bluetooth v5.0', 'PIN', 'White led', 'Stereo speaker',
'364.5 x 245 x 24.2 mm', '24 month', 'Windows 10 Pro', 270000000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Asus Vivobook A415EA EB360', 1, 3, 'Intel Core i5-1135G7', '8GB DDR4 2666MHz Onboard', '512GB SSD M.2 PCIE G3X4', 'Intel Iris Xe Graphics',
'14" FHD (1920 x 1080), IPS, Anti-Glare with 45% NTSC, NanoEdge', '3 Cells 42WHrs', 1.4, 'Plastic and Metal',
'1 x Type-A USB 3.2 Gen 1, 1 x Type-C USB 3.2, 2 x USB 2.0 port(s), 1 x HDMI 1.4','802.11AC (2x2), Bluetooth v5.0', 'PIN, Fingerprint',
'No led', 'Harman Kardon', '324 x 215 x 17.9 mm', '24 month', 'Windows 10 Home', 17490000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('ASUS ZenBook UX325EA KG363T', 1, 3, 'Intel Core i5-1135G7', '8GB 4266MHz LPDDR4X', '512GB PCIe NVMe 3.0 x2 M.2 SSD', 'Intel Iris Xe Graphics',
'13.3", 1920 x 1080 Pixel, OLED, 60 Hz, 400 nits, OLED', '4-cell, 65WHrs', 1.1, 'Plastic and Metal',
'2 x Thunderbolt 4 USB-C (up to 40Gbps), 1 x USB 3.2 Gen 1 Type-A (up to 5Gbps), 1 x Standard HDMI, 1 x MicroSD card reader',
'Intel WiFi 6 with Gig+ performance (802.11ax), Bluetooth v5.0', 'PIN, Fingerprint', 'White led', 'No infor', '30.4 x 20.3 x 13.9 cm',
'24 month', 'Windows 10 Home', 25290000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Acer Aspire 7 A715 42G R4ST', 3, 1, 'AMD Ryzen 5 5500U', '8GB DDR4', '256GB PCIe NVMe M.2 SSD', 'NVIDIA GeForce GTX 1650',
'15.6" FHD (1920 x 1080) IPS, Anti-Glare, 60Hz', '4 Cell 48Whr', 2.1, 'Plastic', '1x USB 3.0, 1x USB Type C, 2x USB 2.0, 1x HDMI, 1x RJ45',
'LAN, Wi-Fi 6(Gig+)(802.11ax) (2x2), Bluetooth v5.0', 'PIN', 'White led', 'True Harmony; Dolby Audio Premium', '363.4 x 254.5 x 23.25 (mm)',
'12 month', 'Windows 10 Home', 18490000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('ConceptD 7 Ezel Pro CC715-91P-X8CX', 3, 4, 'Intel Xeon W-10885M', '32GB DDR4', '2TB SSD PCIe NVMe', 'NVIDIA Quadro RTX 5000 Max Q',
'15.6" UHD (3840 x 2160) Acer ComfyView 4K LED LCD/Pen, Touch', '84Whr', 2.54, 'Metal',
'1x HDMI 2.0, 2x Thunderbolt 3 (USB 3.1 Gen 2), 1x DisplayPort, 1x 3.5mm jack, 1x SD card reader, 1x USB 3.1 Gen 1 port with power-off charging, 1x USB 3.1 Gen 1, 1x RJ-45',
'LAN, Intel Wireless Wi-Fi 6 AX201, 802.11a/b/g/n/acR2+ax wireless, Bluetooth v5.0', 'PIN, Fingerprint', 'White led',
'Acer TrueHarmony technology; Utra Audio', '358.5 (W) x 260 (D) x 22.5/28.6 (H) mm', '12 month', 'Windows 10 Home', 151499000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Acer Nitro 5 Eagle AN515 57 74NU', 3, 1, 'Intel Core i7-11800H', '8GB DDR4 3200MHz', '512GB SSD M.2 PCIE', 'NVIDIA GeForce RTX 3050Ti',
'15.6 inch FHD(1920 x 1080) IPS 144Hz SlimBezel', '4 Cell 57.5WHr', 2.2, 'Plastic', '3x USB 3.1 Gen 1, 1x USB 3.2 Gen 2 Type C, 1x HDMI, RJ45',
'LAN, 802.11AX (2x2), Bluetooth v5.1', 'PIN', 'RGB 4 zone', 'Waves MaxxAudio, Acer TrueHarmony', '363.4 x 255 x 23.9 mm', '24 month', 'Windows 10 Home', 27690000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Acer Predator Helios 300 PH315 53 78TN', 3, 1, 'Intel Core i7-10750H', '16GB (8GBx2) DDR4 3200MHz', '512GB SSD M.2 PCIE G3X4', 'NVIDIA GeForce RTX 3060',
'15.6" FHD (1920 x 1080) IPS, Anti-Glare with 72% NTSC, 100% sRGB, 300Hz, 3ms, 300nits', '4 Cell 59WHr', 2.2, 'Plastic and Metal',
'1x USB 3.2 Gen 2 port featuring power-off USB charging, 2x USB 3.2 Gen 1 ports, 1x RJ 45, 1x HDMI 2.0 port with HDCP support, 1x Mini DisplayPortTM 1.4',
'LAN, Killer Wi-Fi 6 AX1650i, Bluetooth v5.1', 'PIN', 'RGB 4 zone', 'DTS X:Ultra Audio', '363.4 x 255 x 22.9 (mm)', '24 month', 'Windows 10 Home', 39990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Acer Aspire 3 A315 56 37DV', 3, 2, 'Intel Core i3-1005G1', '4GB DDR4 2400MHz Onboard', '256GB SSD M.2 PCIE', 'Intel UHD Graphics',
'15.6" FHD (1920 x 1080) Acer ComfyView LCD, Anti-Glare', '4 Cell 59WHr', 1.7, 'Plastic', '1x USB 3.1, 2x USB 2.0, HDMI, RJ-45',
'LAN, 802.11 ac, Bluetooth v4.2', 'PIN', 'No led', 'Realtek High Definition', '363 x 247.5 x 19.9 (mm)', '12 month', 'Windows 10 Home', 10990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Acer Swift 3 SF314 511 56G1', 3, 3, 'Intel Core i5-1135G7', '16GB LPDDR4X 2666 MHz', '512GB PCIe NVMe SSD', 'Intel Iris Xe Graphics',
'14"Full HD (1920 x 1080) 60Hz IPS', '48 Wh', 1.19, 'Plastic and Metal', '1 Thunderbolt, 4x USB-C, 2x USB 3.2, HDMI, 3.5 mm audio jack',
'Wi-Fi 6 (802.11ax), Bluetooth v5.1', 'PIN, Fingerprint', 'No led', 'Acer TrueHarmonyDTS Audio', '322.8 x 212.2 x 15.9 (mm)', '12 month', 'Windows 10 Home', 19690000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('MSI GF63 Thin 10SC 468VN', 2, 1, 'Intel Core i5-10500H', '8GB DDR4 3200MHz', '512GB SSD M.2 PCIE', 'NVIDIA GeForce GTX 1650',
'15.6" FHD (1920 x 1080) IPS, 144Hz, Thin Bezel', '3 Cell 51WHr', 1.86, 'Plastic', '3x USB3.2 Gen1, 1x Type-C USB3.2 Gen1, HDMI, RJ-45',
'LAN, Intel Wireless-AC 9560 (2*2 AC), Bluetooth v5.0', 'PIN', 'Red led', '2x 2W Speaker', '359 x 254 x 21.7 mm', '24 month', 'Windows 10 Home', 20490000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('MSI GL65 Leopard 10SCXK 093VN', 2, 1, 'Intel Core i7-10750H', '8GB DDR4 3200MHz', '512GB SSD M.2 PCIE', 'NVIDIA GeForce GTX 1650',
'15.6" FHD (1920 x 1080) IPS with Anti-Glare, 144Hz, Thin Bezel, 45% NTSC', '6 Cell 51WHr', 2.3, 'Plastic',
'1x Type-C USB3.2 Gen1, 3x Type-A USB3.2 Gen1, 1x Mini-DisplayPort, 1x (4K @ 30Hz) HDMI, 1x RJ45','LAN, Intel Wi-Fi 6 AX201(2*2 ax), Bluetooth v5.1',
'PIN', 'RGB Per-key', 'Nahimic 3 Audio Technology', '357.7 x 248 x 27.5 mm', '24 month', 'Windows 10 Home', 22690000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('MSI Stealth 15M A11SDK 061VN', 2, 1, 'Intel Core i7-1185G7', '16GB (8GBx2) DDR4 3200MHz', '512GB SSD PCIE G3X4', 'NVIDIA GeForce GTX 1660Ti',
'15.6" FHD (1920*1080), IPS-Level 144Hz 45%NTSC Thin Bezel', '6 Cell 51WHr', 1.69, 'Plastic and Metal',
'1x Type-C (USB4 / DP / Thunderbolt 4) with PD charging, 2x Type-A USB3.2 Gen1, 1x (4K @ 60Hz) HDMI','LAN, Intel Wi-Fi 6 AX201 (2x2), Bluetooth v5.1',
'PIN', 'Led RGB', 'NAHIMIC 3 (2x 2W Speaker)', '358 x 248 x 15.95 (mm)', '24 month', 'Windows 10 Home', 30490000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Lenovo IdeaPad Slim 3 15IIL05', 6, 2, 'Intel Core i3 1005G1', '4GB DDR4 2666Mhz', '512GB SSD PCIE', 'Intel UHD Graphics',
'15.6" FHD (1920*1080), TN panel, Anti-Glare', '35Whrs', 1.85, 'Plastic', '2x USB 3.2, 1x USB 2.0, HDMI, 3.5mm audio jack, 1x Micro SD',
'Wi-Fi 802.11 a/b/g/n/ac, Bluetooth v5.0', 'PIN', 'No Led', 'Stereo speakers', '362.2 mm x 253.4 mm x 19.9 mm', '12 month', 'Windows 10 Home', 12690000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Lenovo Ideapad Slim 5 15ITL05', 6, 3, 'Intel Core i5 1135G7', '16GB DDR4 3200Mhz', '512GB SSD M2 PCIE', 'Intel Iris Xe',
'15.6" FHD (1920*1080), IPS LCD LED Backlit, True Tone, 45% NTSC, 300 nits', '2 cell 45Whrs', 1.66, 'Plastic and metal',
'1x Type-C, 2x USB 3.0, 1x HDMI, 1x SD Card reader, 1x Jack 3.5 mm', 'Wifi 802.11 ax, Bluetooth v5.1', 'PIN, Fingerprint, Face unlock',
'White Led', 'Dolby audio', '356.67 mm x 233.13 mm x 17.9 mm', '12 month', 'Windows 10 Home', 20199000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Lenovo Legion 5-15ACH6', 6, 1, 'AMD Ryzen 7 5600H', '8GB DDR4 3200MHz', '512GB SSD M.2 2280 NVMe', 'NVIDIA GeForce RTX 3050',
'15.6” FHD IPS 300nits 165Hz 100% sRGB', '60Whrs', 2.4, 'Plastic',
'3x USB 3.2 Gen 1, 1x Ethernet (RJ-45), 1x USB 3.2 Gen 1, 1x USB-C 3.2 Gen 2 (support data transfer and DisplayPort 1.4), 1x headphone / microphone combo jack (3.5mm), 1x HDMI 2.1',
'LAN, Wifi 802.11 ax, Bluetooth v5.1', 'PIN', 'White Led', 'High Definition (HD) Audio, Realtek ALC3306 codec', '362.56 x 260.61 x 22.5-25.75 mm',
'24 month', 'Windows 10 Home', 28789000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Lenovo Thinkpad X1 Fold Gen 1', 6, 3, 'Intel Core i5 L16G7', '8GB LPDDR4x-4266', '512GB SSD M2 PCIE', 'Intel UHD Graphics',
'13.3 QXGA (2048x1536), Multi touch, Fodding OLed, 300nits', 'No infor', 1, 'Plastic and metal',
'2x USB-C 3.2 Gen 2 (support data transfer, Power Delivery and DisplayPort 1.2), Optional Ports, 1x Nano-SIM card slot (WWAN models)',
'5G, Intel Wi-Fi 6 AX200, 802.11ax 2x2, Bluetooth 5.2', 'PIN, Fingerprint, Face unlock', 'No led', 'No infor',
'Folded: 158 x 235.6 x 27.8 mm, Unfolded: 299 x 235.6 x 11.3 mm', '36 month', 'Windows 10 Pro', 60599000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Lenovo ThinkPad X1 Carbon Gen 8 X1CG806NO', 6, 3, 'Intel Core i5 10210U', '16GB LPDDR3 2133 MHz', '512GB SSD M2 PCIE', 'Intel UHD Graphics',
'14" FHD IPS (1920 x 1080), anti-glare (400nits), 92% sRGB', '51WHrs', 1.1, 'Metal', '2 x USB-A, 2 x USB-C with Thunderbolt 3.0, 1 x Micro-SD',
'Intel Wi-Fi 6 AX200, 802.11ax 2x2 Wi-Fi, Bluetooth v5.2', 'PIN, Fingerprint, Face unlock', 'No led', '2x 2W speakers', '323mm x 217mm x 14.9mm',
'12 month', 'Windows 10 Pro', 29990000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Dell XPS 13 9310 (70231343)', 7, 3, 'Intel Core i5 1135G7', '8GB LPDDR4x 4267 MHz', '256GB PCIe NVMe SSD', 'Intel Iris Xe Graphics',
'13.4 inch, FHD+ (1920 x 1200) Touch, 60 Hz, anti-glare', '4 cell 52WHrs', 1.27, 'Metal',
'2 Thunderbolt 4 ports; 1 headset (headphone and microphone combo) port, Micro-SD',
'Killer AX1650, 2x2 MIMO, 2.4/5 GHz, Wi-Fi 6 (WiFi 802.11ax), Bluetooth 5.1', 'PIN, Fingerprint, Face unlock', 'No led',
'Waves MaxxAudio Pro, 2W Stereo speaker', '14.8 x 295.7 x 198.7 (mm)', '12 month', 'Windows 10 Home', 39389000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Dell Latitude 3520 (70251603)', 7, 2, 'Intel Core i3 1115G4', '4GB DDR4 3200Mhz', '256GB PCIe NVMe SSD', 'Intel UHD Graphics',
'15.6 inch HD (1366 x 768) WVA Anti-glare 60Hz', '3 cell 41WHrs', 1.79, 'Plastic',
'Audio jack , 1 x USB 2.0, 1 x  USB 3.2 Gen 1 Type-A port 1 x RJ-45, 1 x HDMI 1.4 , 1 x USB 3.2 Gen 1 Type-A port PowerShare, 1 x USB 3.2 Gen2x2 Type-C port',
'LAN, Intel Wi-Fi 6 AX201, 2 X 2, 802.11ax with Bluetooth 5.1', 'PIN', 'No led', '2x 2W Stereo speaker', '326 x 226 x 18.6mm', '12 month', 'Fedora', 14439000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('LG Gram 14ZD90P-G.AX51A5', 5, 3, 'Intel Core i5-1135G7', '8GB LPDDR4X 4266MHz', '256GB PCIe NVMe SSD', 'Intel Iris Xe Graphics',
'14.0 inch (30.2cm) WUXGA (1920*1200) IPS LCD', '72WHrs', 1, 'Metal',
'HP-Out(4Pole Headset, US type), USB 3.2 Gen2x1 (x2), HDMI,  USB 4 Gen3x2 Type C (x2, with Power Delivery, Display Port, Thunderbolt 4)',
'Intel Wi-Fi 6 AX201D2W; Bluetooth 5.0', 'PIN, Fingerprint', 'No led', 'DTS: X Ultra', '313.4 x 215.2 x 16.8  mm', '12 month', 'No OS', 14439000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Macbook Pro 13 Touchbar Z11D000E5', 4, 3, 'Apple M1', '16GB', '256GB PCIe NVMe SSD', '8 core GPU',
'Retina 13.3 inch (2560x1600) IPS Led Backlit True Tone', '58.2WHrs', 1.4, 'Metal', 'USB 3.1 Gen2, 2x Thunder Bolt 3',
'Wifi 802.11ac - Bluetooth 5.0', 'PIN, Touch ID', 'No led', 'Stereo speakers', '304.1 x 212.4 x 15.6  mm', '12 month', 'MAC OS', 38989000, 50);

INSERT INTO laptops(laptop_name, manufactory_id, category_id, CPU, Ram, hard_drive, VGA, display, battery, weight, materials, ports,
			 network_and_connection, security, keyboard, audio, size, warranty_period, OS, price, quantity)
VALUES ('Macbook Air 13 MVFM2', 4, 3, 'Intel Core i5 8th', '8GB LPDDR3', '128 PCIe NVMe SSD', 'Intel UHD 617',
'Retina 13.3 inch (2560x1600) IPS Led Backlit True Tone', '49.9WHrs', 1.25, 'Metal', 'USB 3.1 Gen2, 2x Thunder Bolt 3',
'Wifi 802.11ac - Bluetooth 4.2', 'PIN, Touch ID', 'No led', 'Stereo speakers', '304.1 x 212.4 x 4.1  mm', '12 month', 'MAC OS', 27259000, 50);

INSERT INTO orders(order_id, seller_id, customer_id, order_status)
VALUES(1, 1, 1, 1), (2, 2, 2, 1), (3, 3, 3, 1), (4, 1, 1, 1), (5, 2, 2, 1), (6, 3, 3, 1);

INSERT INTO order_details(order_id, laptop_id, quantity, unit_price)
VALUES(1, 1, 2, 24990000), (2, 1, 2, 24990000), (3, 1, 2, 24990000), (1, 27, 3, 27259000), (2, 27, 1, 27259000), (3, 27, 2, 27259000),
	  (4, 1, 2, 24990000), (5, 1, 2, 24990000), (6, 1, 2, 24990000), (4, 27, 1, 27259000), (5, 27, 1, 27259000), (6, 27, 1, 27259000);
      
DELIMITER $$
CREATE PROCEDURE sp_login(IN username VARCHAR(255), IN password VARCHAR(255))
BEGIN
SELECT
	s.staff_id, s.staff_name, s.username, s.password, s.role
FROM staffs s
WHERE s.username = BINARY username AND s.password = BINARY password;
END $$
DELIMITER ;
-- end sp_login

DELIMITER $$
CREATE PROCEDURE sp_getStaffById(IN id INT)
BEGIN
SELECT
	s.staff_id, s.staff_name, s.username, s.password, s.role
FROM staffs s
WHERE staff_id = id;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE sp_createCustomer(IN customerName VARCHAR(50), IN address VARCHAR(100), 
 IN phone VARCHAR(10))
BEGIN
INSERT INTO Customers(customer_name, address, phone)
VALUES(customerName, address, phone);
END $$
DELIMITER ;
-- end sp_createCustomer 

DELIMITER $$
CREATE PROCEDURE sp_getCustomerByPhone(IN phone VARCHAR(10))
BEGIN
SELECT 
	c.customer_id, c.customer_name, c.phone, c.address
FROM customers c
WHERE c.phone = phone;
END $$
DELIMITER ;
-- end sp_getCustomerByPhone 

DELIMITER $$
CREATE PROCEDURE sp_getNewCustomerId()
BEGIN
SELECT customer_id FROM customers ORDER BY customer_id DESC LIMIT 1;
END $$
DELIMITER ;
-- end sp_getNewCustomerId
-- end sp_getNewCustomerId


DELIMITER $$
CREATE PROCEDURE sp_getLaptopById(IN laptopId int)
BEGIN 
SELECT 
    l.laptop_id, l.laptop_name, c.category_id, c.category_name, m.manufactory_id, m.manufactory_name, IFNULL(m.website, '') AS website,
    IFNULL(m.address, '') AS address, l.CPU, l.Ram, l.hard_drive, l.VGA, l.display, l.battery, l.weight,l.materials, l.ports,
    l.network_and_connection, l.security, l.keyboard, l.audio, l.size, l.warranty_period, l.OS, l.price, l.quantity
FROM
    laptops l
        INNER JOIN categories c ON l.category_id = c.category_id
        INNER JOIN manufactories m ON l.manufactory_id = m.manufactory_id
WHERE l.laptop_id = laptopId;
END $$
DELIMITER ;
-- end sp_getLaptopById

DELIMITER $$
CREATE PROCEDURE sp_searchLaptops(IN searchValue VARCHAR(255))
BEGIN 
SELECT 
	l.laptop_id, l.laptop_name, c.category_id, c.category_name, m.manufactory_id, m.manufactory_name, IFNULL(m.website, '') AS website,
    IFNULL(m.address, '') AS address, l.CPU, l.Ram, l.hard_drive, l.VGA, l.display, l.battery, l.weight,l.materials, l.ports,
    l.network_and_connection, l.security, l.keyboard, l.audio, l.size, l.warranty_period, l.OS, l.price, l.quantity
FROM
    laptops l
        INNER JOIN categories c ON l.category_id = c.category_id
        INNER JOIN manufactories m ON l.manufactory_id = m.manufactory_id
WHERE l.laptop_name LIKE CONCAT('%', searchValue, '%') OR 
	  c.category_name LIKE CONCAT('%', searchValue, '%') OR
      m.manufactory_name LIKE  CONCAT('%', searchValue, '%') OR
      l.laptop_id = searchValue
ORDER BY l.laptop_id;
END $$
DELIMITER ;
-- end sp_searchLaptops

DELIMITER $$
CREATE PROCEDURE sp_getPrice(IN laptopId INT)
BEGIN
	SELECT price FROM laptops WHERE laptop_id = laptopID;
END $$
DELIMITER ;
-- end sp_getPrice

DELIMITER $$
CREATE PROCEDURE sp_updateQuantityInLaptopsAfterCreateOrder(IN _quantity INT, IN laptopId INT)
BEGIN
UPDATE LAPTOPS SET quantity = quantity - _quantity WHERE laptop_id = laptopId;
END $$
DELIMITER ;
-- end sp_updateQuantityInLaptopsAfterCreateOrder

DELIMITER $$
CREATE PROCEDURE sp_updateQuantityInLaptopsAfterCancelOrder(IN _quantity INT, IN laptopId INT)
BEGIN
UPDATE LAPTOPS SET quantity = quantity + _quantity WHERE laptop_id = laptopId;
END $$
DELIMITER ;
-- end sp_updateQuantityInLaptopsAfterCancelOrder


DELIMITER $$
CREATE PROCEDURE sp_createOrder(IN customerId INT, IN sellerId INT, IN orderStatus INT)
BEGIN
INSERT INTO orders(customer_id, seller_id, order_date, order_status)
VALUES(customerId, sellerId, NOW(), orderStatus);
END $$
DELIMITER ;
-- end sp_createOrder

DELIMITER $$
CREATE PROCEDURE sp_getNewOrderId()
BEGIN
SELECT order_id FROM orders ORDER BY order_id DESC LIMIT 1;
END $$
DELIMITER ;
-- end sp_getNewOrderId

DELIMITER $$
CREATE PROCEDURE sp_insertToOrderDetails(IN orderId INT, IN laptopId INT, IN price DECIMAL, IN quantity INT)
BEGIN
INSERT INTO order_details(order_id, laptop_id, unit_price, quantity)
VALUES (orderId, laptopId, price, quantity);
END $$
DELIMITER ;
-- end sp_insertToOrderDetails

DELIMITER $$
CREATE PROCEDURE sp_getOrdersUnpaid()
BEGIN
SELECT 
    o.order_id, o.order_date, o.order_status, c.customer_id, c.customer_name, c.phone, c.address, sl.staff_id AS seller_id,
    sl.staff_name AS seller_name, ac.staff_id AS accountant_id, ifnull(ac.staff_name,'') AS accountant_name
FROM
    orders o
        INNER JOIN customers c ON o.customer_id = c.customer_id
        LEFT JOIN staffs sl ON o.seller_id = sl.staff_id
        LEFT JOIN staffs ac ON o.accountant_id = ac.staff_id
WHERE o.order_status = 1 OR o.order_status = 2
ORDER BY o.order_status;
END $$
DELIMITER ;
-- end sp_getOrderUnpaid

DELIMITER $$
CREATE PROCEDURE sp_getOrderById(IN orderId int)
BEGIN  
SELECT 
    o.order_id, o.order_date, o.order_status, c.customer_id, c.customer_name, c.phone, c.address, sl.staff_id AS seller_id,
    sl.staff_name AS seller_name, ac.staff_id AS accountant_id, ifnull(ac.staff_name,'') AS accountant_name
FROM
    orders o
        INNER JOIN customers c ON o.customer_id = c.customer_id
        LEFT JOIN staffs sl ON o.seller_id = sl.staff_id
        LEFT JOIN staffs ac ON o.accountant_id = ac.staff_id
WHERE o.order_id = orderId;
END $$
DELIMITER ;
-- end sp_getOrderById

DELIMITER $$
CREATE PROCEDURE sp_getLaptopsInOrder(IN orderId INT)
BEGIN  
SELECT 
    od.laptop_id, od.quantity, od.unit_price as price, l.laptop_name, c.category_id, c.category_name, m.manufactory_id, m.manufactory_name, 
    IFNULL(m.website, '') AS website, IFNULL(m.address, '') AS address, l.CPU, l.Ram, l.hard_drive, l.VGA, l.display, l.battery, 
    l.weight, l.materials, l.ports, l.network_and_connection, l.security, l.keyboard, l.audio, l.size, l.warranty_period, l.OS
FROM
    order_details od
    INNER JOIN laptops l ON od.laptop_id = l.laptop_id
    INNER JOIN categories c ON l.category_id = c.category_id
	INNER JOIN manufactories m ON l.manufactory_id = m.manufactory_id
WHERE od.order_id = orderId;
END $$
DELIMITER ;
-- end sp_getLaptopsInOrder

DELIMITER $$
CREATE PROCEDURE sp_updateOrderAfterPayment(IN staffId INT, IN orderStatus INT, IN orderId INT)
BEGIN
	UPDATE orders SET order_status = orderStatus, accountant_id = staffId WHERE order_id = orderId;
END $$
DELIMITER ;
-- end sp_updateOrderAfterPayment

DELIMITER $$
CREATE PROCEDURE sp_changeOrderStatus(IN status INT, IN orderId INT, IN staffId INT)
BEGIN
UPDATE orders SET order_status = status, accountant_id = staffId WHERE order_id = orderId;
END $$
DELIMITER ;
-- end sp_sp_changeOrderStatus


DELIMITER $$
CREATE TRIGGER tg_beforeInsert BEFORE INSERT
	ON laptops FOR EACH ROW
    BEGIN
		IF NEW.quantity < 0 THEN
            SIGNAL SQLSTATE '45001' SET MESSAGE_TEXT = 'tg_beforeInsert: Quantity must > 0';
        END IF;
    END $$
DELIMITER ;

delimiter $$
CREATE TRIGGER tg_checkQuantity
	BEFORE UPDATE ON laptops
	FOR EACH ROW
	BEGIN
		IF NEW.quantity < 0 THEN
            SIGNAL SQLSTATE '45001' SET MESSAGE_TEXT = 'tg_checkQuantity: Quantity must > 0';
        END IF;
    END $$
DELIMITER ;

DROP EVENT IF EXISTS update_order;
CREATE EVENT update_order  ON SCHEDULE EVERY 1 MINUTE 
STARTS '2010-01-01 00:00:00'
DO
UPDATE orders set order_status = 4 where (hour(now()) = 0 AND minute(Now() = 0)) AND (order_status = 1 OR order_status = 2);

DROP USER IF EXISTS 'laptop'@'localhost';
CREATE USER IF NOT EXISTS 'laptop'@'localhost' IDENTIFIED BY 'vtcacademy';

GRANT EXECUTE ON PROCEDURE laptop_store.sp_login TO 'laptop'@'localhost'; 
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getStaffById TO 'laptop'@'localhost'; 
GRANT EXECUTE ON PROCEDURE laptop_store.sp_createCustomer TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getCustomerByPhone TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getNewCustomerId TO 'laptop'@'localhost';

GRANT EXECUTE ON PROCEDURE laptop_store.sp_searchLaptops TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getLaptopById TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getPrice TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_updateQuantityInLaptopsAfterCreateOrder TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_updateQuantityInLaptopsAfterCancelOrder TO 'laptop'@'localhost';

GRANT EXECUTE ON PROCEDURE laptop_store.sp_createOrder TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getNewOrderId TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_insertToOrderDetails TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getOrdersUnpaid TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getOrderById TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_getLaptopsInOrder TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_changeOrderStatus TO 'laptop'@'localhost';
GRANT EXECUTE ON PROCEDURE laptop_store.sp_updateOrderAfterPayment TO 'laptop'@'localhost';

GRANT LOCK TABLES ON laptop_store.* TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.customers TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.laptops TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.orders TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.categories TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.manufactories TO 'laptop'@'localhost';
GRANT SELECT ON laptop_store.order_details TO 'laptop'@'localhost';

SELECT 
	l.laptop_id, l.laptop_name, c.category_id, c.category_name, m.manufactory_id, m.manufactory_name, IFNULL(m.website, '') AS website,
    IFNULL(m.address, '') AS address, l.CPU, l.Ram, l.hard_drive, l.VGA, l.display, l.battery, l.weight,l.materials, l.ports,
    l.network_and_connection, l.security, l.keyboard, l.audio, l.size, l.warranty_period, l.OS, l.price, l.quantity
FROM
    laptops l
        INNER JOIN categories c ON l.category_id = c.category_id
        INNER JOIN manufactories m ON l.manufactory_id = m.manufactory_id
WHERE l.laptop_name LIKE CONCAT('%', 'a', '%') OR 
	  c.category_name LIKE CONCAT('%', 'a', '%') 
      ORDER BY l.price desc;