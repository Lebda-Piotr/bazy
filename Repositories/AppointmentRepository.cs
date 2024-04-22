﻿using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace bazy1.Repositories
{
    public class AppointmentRepository
    {
        private string connectionString;

        public AppointmentRepository()
        {
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            try
            {
                string json = System.IO.File.ReadAllText("dbInfo.json");
                dynamic jsonObj = JsonSerializer.Deserialize<dynamic>(json);
                connectionString = jsonObj["ConnectionStrings"]["BazaPrzychodnia"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error laczenie z baza: " + ex.Message);
            }
        }

        public List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM appointments";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                DateTime = reader["DateTime"].ToString(),
                                Goal = reader["Goal"].ToString(),
                                NotificationId = Convert.ToInt32(reader["NotificationId"]),
                                PatientId = Convert.ToInt32(reader["PatientId"]),
                                // wczytywanie relacji z innymi tabelami, jesli bedzie trzeba
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sciaganie appointments z bazy: " + ex.Message);
            }
            return appointments;
        }
        public bool AddAppointment(Appointment appointment)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO appointments (DateTime, Goal, NotificationId, PatientId) " +
                                   "VALUES (@DateTime, @Goal, @NotificationId, @PatientId)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DateTime", appointment.DateTime);
                    cmd.Parameters.AddWithValue("@Goal", appointment.Goal);
                    cmd.Parameters.AddWithValue("@NotificationId", appointment.NotificationId);
                    cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error dodawanie appointment do bazy: " + ex.Message);
                return false;
            }
        }

        // Metody do edycji, usuwania wizyt, itp. // 30.04
    }
}

