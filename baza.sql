-- MySQL dump 10.13  Distrib 8.3.0, for Win64 (x86_64)
--
-- Host: localhost    Database: przychodnia9
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `address` (
  `id` int NOT NULL AUTO_INCREMENT,
  `street` varchar(45) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `buildingNumber` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrator` (
  `id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(45) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Administrator_User1_idx` (`User_id`),
  CONSTRAINT `fk_Administrator_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointment`
--

DROP TABLE IF EXISTS `appointment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` varchar(45) DEFAULT NULL,
  `goal` varchar(45) DEFAULT NULL,
  `Notification_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`id`,`Notification_id`,`Patient_id`),
  KEY `fk_Appointment_Patient1_idx` (`Patient_id`),
  CONSTRAINT `fk_Appointment_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment`
--

LOCK TABLES `appointment` WRITE;
/*!40000 ALTER TABLE `appointment` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointment_has_receptionist`
--

DROP TABLE IF EXISTS `appointment_has_receptionist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment_has_receptionist` (
  `Appointment_id` int NOT NULL,
  `Appointment_Notification_id` int NOT NULL,
  `Appointment_Patient_id` int NOT NULL,
  `Receptionist_id` int NOT NULL,
  `Receptionist_User_id` int NOT NULL,
  PRIMARY KEY (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`,`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_Appointment_has_Receptionist_Receptionist1_idx` (`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_Appointment_has_Receptionist_Appointment1_idx` (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  CONSTRAINT `fk_Appointment_has_Receptionist_Appointment1` FOREIGN KEY (`Appointment_id`, `Appointment_Notification_id`, `Appointment_Patient_id`) REFERENCES `appointment` (`id`, `Notification_id`, `Patient_id`),
  CONSTRAINT `fk_Appointment_has_Receptionist_Receptionist1` FOREIGN KEY (`Receptionist_id`, `Receptionist_User_id`) REFERENCES `receptionist` (`id`, `User_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment_has_receptionist`
--

LOCK TABLES `appointment_has_receptionist` WRITE;
/*!40000 ALTER TABLE `appointment_has_receptionist` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointment_has_receptionist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `category_id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `disease`
--

DROP TABLE IF EXISTS `disease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `disease` (
  `name` varchar(45) NOT NULL,
  `dateFrom` varchar(45) DEFAULT NULL,
  `dateTo` varchar(45) DEFAULT NULL,
  `isEnded` tinyint DEFAULT NULL,
  `comments` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disease`
--

LOCK TABLES `disease` WRITE;
/*!40000 ALTER TABLE `disease` DISABLE KEYS */;
/*!40000 ALTER TABLE `disease` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor`
--

DROP TABLE IF EXISTS `doctor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Doctor_User1_idx` (`User_id`),
  CONSTRAINT `fk_Doctor_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor`
--

LOCK TABLES `doctor` WRITE;
/*!40000 ALTER TABLE `doctor` DISABLE KEYS */;
INSERT INTO `doctor` VALUES (1,'fsd','dsfsd',NULL,8),(2,'Adam','Kowalski',NULL,10),(3,'anna','kowalska',NULL,11),(4,'Adam','Nowak',NULL,12),(5,'zx','zx',NULL,13),(6,'asd','sad',NULL,14);
/*!40000 ALTER TABLE `doctor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_has_office`
--

DROP TABLE IF EXISTS `doctor_has_office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_has_office` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Office_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Office_id`),
  KEY `fk_Doctor_has_Office_Office1_idx` (`Office_id`),
  KEY `fk_Doctor_has_Office_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Office_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Office_Office1` FOREIGN KEY (`Office_id`) REFERENCES `office` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_has_office`
--

LOCK TABLES `doctor_has_office` WRITE;
/*!40000 ALTER TABLE `doctor_has_office` DISABLE KEYS */;
/*!40000 ALTER TABLE `doctor_has_office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_has_patient`
--

DROP TABLE IF EXISTS `doctor_has_patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_has_patient` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Patient_id`),
  KEY `fk_Doctor_has_Patient_Patient1_idx` (`Patient_id`),
  KEY `fk_Doctor_has_Patient_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Patient_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Patient_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_has_patient`
--

LOCK TABLES `doctor_has_patient` WRITE;
/*!40000 ALTER TABLE `doctor_has_patient` DISABLE KEYS */;
/*!40000 ALTER TABLE `doctor_has_patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_specialization`
--

DROP TABLE IF EXISTS `doctor_specialization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_specialization` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Specialization_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Specialization_id`),
  KEY `fk_Doctor_has_Specialization_Specialization1_idx` (`Specialization_id`),
  KEY `fk_Doctor_has_Specialization_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Specialization_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Specialization_Specialization1` FOREIGN KEY (`Specialization_id`) REFERENCES `specialization` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_specialization`
--

LOCK TABLES `doctor_specialization` WRITE;
/*!40000 ALTER TABLE `doctor_specialization` DISABLE KEYS */;
INSERT INTO `doctor_specialization` VALUES (1,8,1),(2,10,1),(3,11,2),(4,12,2),(5,13,2),(6,14,2);
/*!40000 ALTER TABLE `doctor_specialization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medicine`
--

DROP TABLE IF EXISTS `medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicine` (
  `name` int DEFAULT NULL,
  `dose` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicine`
--

LOCK TABLES `medicine` WRITE;
/*!40000 ALTER TABLE `medicine` DISABLE KEYS */;
/*!40000 ALTER TABLE `medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notification`
--

DROP TABLE IF EXISTS `notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notification` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  `hour` time DEFAULT NULL,
  `isSent` tinyint DEFAULT NULL,
  `Appointment_id` int NOT NULL,
  `Appointment_Notification_id` int NOT NULL,
  `Appointment_Patient_id` int NOT NULL,
  PRIMARY KEY (`id`,`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  KEY `fk_Notification_Appointment1_idx` (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  CONSTRAINT `fk_Notification_Appointment1` FOREIGN KEY (`Appointment_id`, `Appointment_Notification_id`, `Appointment_Patient_id`) REFERENCES `appointment` (`id`, `Notification_id`, `Patient_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
/*!40000 ALTER TABLE `notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `office`
--

DROP TABLE IF EXISTS `office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `office` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Number` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `office`
--

LOCK TABLES `office` WRITE;
/*!40000 ALTER TABLE `office` DISABLE KEYS */;
/*!40000 ALTER TABLE `office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient` (
  `Pesel` int DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_address`
--

DROP TABLE IF EXISTS `patient_address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_address` (
  `Patient_id` int NOT NULL,
  `Address_id` int NOT NULL,
  PRIMARY KEY (`Patient_id`,`Address_id`),
  KEY `fk_Patient_has_Address_Address1_idx` (`Address_id`),
  KEY `fk_Patient_has_Address_Patient1_idx` (`Patient_id`),
  CONSTRAINT `fk_Patient_has_Address_Address1` FOREIGN KEY (`Address_id`) REFERENCES `address` (`id`),
  CONSTRAINT `fk_Patient_has_Address_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_address`
--

LOCK TABLES `patient_address` WRITE;
/*!40000 ALTER TABLE `patient_address` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient_address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_diesease`
--

DROP TABLE IF EXISTS `patient_diesease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_diesease` (
  `Disease_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`Disease_id`,`Patient_id`),
  KEY `fk_Disease_has_Patient_Patient1_idx` (`Patient_id`),
  KEY `fk_Disease_has_Patient_Disease1_idx` (`Disease_id`),
  CONSTRAINT `fk_Disease_has_Patient_Disease1` FOREIGN KEY (`Disease_id`) REFERENCES `disease` (`id`),
  CONSTRAINT `fk_Disease_has_Patient_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_diesease`
--

LOCK TABLES `patient_diesease` WRITE;
/*!40000 ALTER TABLE `patient_diesease` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient_diesease` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_medicine`
--

DROP TABLE IF EXISTS `patient_medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_medicine` (
  `Medicine_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`Medicine_id`,`Patient_id`),
  KEY `fk_Medicine_has_Patient_Patient1_idx` (`Patient_id`),
  KEY `fk_Medicine_has_Patient_Medicine1_idx` (`Medicine_id`),
  CONSTRAINT `fk_Medicine_has_Patient_Medicine1` FOREIGN KEY (`Medicine_id`) REFERENCES `medicine` (`id`),
  CONSTRAINT `fk_Medicine_has_Patient_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_medicine`
--

LOCK TABLES `patient_medicine` WRITE;
/*!40000 ALTER TABLE `patient_medicine` DISABLE KEYS */;
/*!40000 ALTER TABLE `patient_medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription`
--

DROP TABLE IF EXISTS `prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription` (
  `id` int NOT NULL AUTO_INCREMENT,
  `dateOfPrescription` date DEFAULT NULL,
  `expirationDate` date DEFAULT NULL,
  `code` varchar(45) DEFAULT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`id`,`Patient_id`),
  UNIQUE KEY `idRecepty_UNIQUE` (`id`),
  KEY `fk_Prescription_Patient1_idx` (`Patient_id`),
  CONSTRAINT `fk_Prescription_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
/*!40000 ALTER TABLE `prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription_medicine`
--

DROP TABLE IF EXISTS `prescription_medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription_medicine` (
  `Prescription_id` int NOT NULL,
  `Prescription_Patient_id` int NOT NULL,
  `Medicine_id` int NOT NULL,
  PRIMARY KEY (`Prescription_id`,`Prescription_Patient_id`,`Medicine_id`),
  KEY `fk_Prescription_has_Medicine_Medicine1_idx` (`Medicine_id`),
  KEY `fk_Prescription_has_Medicine_Prescription1_idx` (`Prescription_id`,`Prescription_Patient_id`),
  CONSTRAINT `fk_Prescription_has_Medicine_Medicine1` FOREIGN KEY (`Medicine_id`) REFERENCES `medicine` (`id`),
  CONSTRAINT `fk_Prescription_has_Medicine_Prescription1` FOREIGN KEY (`Prescription_id`, `Prescription_Patient_id`) REFERENCES `prescription` (`id`, `Patient_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription_medicine`
--

LOCK TABLES `prescription_medicine` WRITE;
/*!40000 ALTER TABLE `prescription_medicine` DISABLE KEYS */;
/*!40000 ALTER TABLE `prescription_medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receptionist`
--

DROP TABLE IF EXISTS `receptionist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receptionist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Receptionist_User1_idx` (`User_id`),
  CONSTRAINT `fk_Receptionist_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receptionist`
--

LOCK TABLES `receptionist` WRITE;
/*!40000 ALTER TABLE `receptionist` DISABLE KEYS */;
/*!40000 ALTER TABLE `receptionist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialization`
--

DROP TABLE IF EXISTS `specialization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `specialization` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialization`
--

LOCK TABLES `specialization` WRITE;
/*!40000 ALTER TABLE `specialization` DISABLE KEYS */;
INSERT INTO `specialization` VALUES (1,'Chirurgia'),(2,'Onkologia'),(3,'Alergologia');
/*!40000 ALTER TABLE `specialization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` enum('recepcjonista','admin','lekarz') NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `hash` varchar(512) DEFAULT NULL,
  `firstLogin` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'admin','admin','admin','as','sa','0',1),(5,'lekarz','fdfdfd','123','qweqwqeq','qweeqeqeqrty','doctor',0),(6,'lekarz','adasd','0000\0\0','abc','cds',NULL,1),(7,'recepcjonista','fe','176112147214O','cx','gr',NULL,1),(8,'lekarz','fa','7847U91','ab','sa',NULL,1),(9,'lekarz','b','a','b','b',NULL,0),(10,'lekarz','AK02615','123','Adam','Kowalski','1',0),(11,'lekarz','ak04103','anna123','anna','kowalska','0',0),(12,'lekarz','an00227','6622LVe','Adam','Nowak','$2a$11$SKxMUhxTaZaP23QPkkSS8.wriA1TYM5nDjK8jWy6FdJ0ibsmuE13C',1),(13,'lekarz','zz05353','lmao','zx','zx','$2a$11$2ureyDLn/FEzCJ74NhAG0.BNXagUAeFxG7DcoZF3O.ohfAr08uqS2',0),(14,'lekarz','as04865','1382!.4','asd','sad','$2a$11$MmditWc.JWH.N6TYJVAr/.Q6K4l2UtYFSz/pLjFdoDH9ZSX6GZen.',0);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workhours`
--

DROP TABLE IF EXISTS `workhours`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workhours` (
  `id` int NOT NULL AUTO_INCREMENT,
  `start` datetime(5) DEFAULT NULL,
  `end` datetime(5) DEFAULT NULL,
  `Receptionist_id` int NOT NULL,
  `Receptionist_User_id` int NOT NULL,
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  PRIMARY KEY (`id`,`Receptionist_id`,`Receptionist_User_id`,`Doctor_id`,`Doctor_User_id`),
  KEY `fk_WorkHours_Receptionist1_idx` (`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_WorkHours_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_WorkHours_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_WorkHours_Receptionist1` FOREIGN KEY (`Receptionist_id`, `Receptionist_User_id`) REFERENCES `receptionist` (`id`, `User_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workhours`
--

LOCK TABLES `workhours` WRITE;
/*!40000 ALTER TABLE `workhours` DISABLE KEYS */;
/*!40000 ALTER TABLE `workhours` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-04 18:15:28
