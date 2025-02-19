-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 19, 2025 at 05:13 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bogsy`
--

-- --------------------------------------------------------

--
-- Table structure for table `customer`
--

CREATE TABLE `customer` (
  `id` int(10) NOT NULL,
  `name` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `phoneNumber` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `customer`
--

INSERT INTO `customer` (`id`, `name`, `email`, `phoneNumber`) VALUES
(1, 'Ron', 'ronrendon@gmail.com', '09123456789'),
(2, 'Tristan', 'tristancancino@gmail.com', '09987654321'),
(3, 'Angelo', 'angelomiho@gmail.com', '09214365879');

-- --------------------------------------------------------

--
-- Table structure for table `rental`
--

CREATE TABLE `rental` (
  `rentalId` int(10) NOT NULL,
  `id` int(10) NOT NULL,
  `videoId` int(10) NOT NULL,
  `rentalDate` date NOT NULL,
  `returnDate` date NOT NULL,
  `actualReturnDate` date DEFAULT NULL,
  `rentalFee` int(11) NOT NULL,
  `lateFee` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `rental`
--

INSERT INTO `rental` (`rentalId`, `id`, `videoId`, `rentalDate`, `returnDate`, `actualReturnDate`, `rentalFee`, `lateFee`) VALUES
(1, 2, 2, '2025-02-19', '2025-02-22', NULL, 50, 0),
(2, 1, 1, '2025-02-19', '2025-02-19', NULL, 25, 0),
(3, 3, 4, '2025-02-19', '2025-02-22', NULL, 50, 0),
(4, 3, 4, '2025-02-19', '2025-02-22', NULL, 50, 0);

-- --------------------------------------------------------

--
-- Table structure for table `video`
--

CREATE TABLE `video` (
  `videoId` int(10) NOT NULL,
  `title` varchar(50) NOT NULL,
  `category` enum('VCD','DVD') NOT NULL,
  `available` int(10) NOT NULL,
  `rented` int(10) NOT NULL,
  `rentalDaysLimit` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `video`
--

INSERT INTO `video` (`videoId`, `title`, `category`, `available`, `rented`, `rentalDaysLimit`) VALUES
(1, 'Madagascar', 'VCD', 1, 2, 3),
(2, 'Mr. and Mrs. Smith', 'DVD', 3, 1, 3),
(4, 'Spider-man', 'DVD', 4, 1, 3);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `rental`
--
ALTER TABLE `rental`
  ADD PRIMARY KEY (`rentalId`),
  ADD KEY `id` (`id`),
  ADD KEY `videoId` (`videoId`);

--
-- Indexes for table `video`
--
ALTER TABLE `video`
  ADD PRIMARY KEY (`videoId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customer`
--
ALTER TABLE `customer`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `rental`
--
ALTER TABLE `rental`
  MODIFY `rentalId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `video`
--
ALTER TABLE `video`
  MODIFY `videoId` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `rental`
--
ALTER TABLE `rental`
  ADD CONSTRAINT `rental_ibfk_1` FOREIGN KEY (`id`) REFERENCES `customer` (`id`),
  ADD CONSTRAINT `rental_ibfk_2` FOREIGN KEY (`videoId`) REFERENCES `video` (`videoId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
