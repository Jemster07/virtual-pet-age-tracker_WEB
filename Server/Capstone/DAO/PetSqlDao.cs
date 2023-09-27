using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Vpat.Models;

namespace Vpat.DAO
{
	public class PetSqlDao : IPetDao
	{
        private readonly string connectionString;

        public PetSqlDao(string dbConnectionString)
		{
            connectionString = dbConnectionString;
		}

        public Pet ReadPet(int petId)
        {
            Pet pet = null;
            string sql = "SELECT pet_id, pet_name, pet_type, brand, date_birth, time_birth, " +
                "date_death, is_active, is_hidden, user_id FROM pets WHERE pet_id = @petId";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@petId", petId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        pet = GetPetFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return pet;
        }

        public List<Pet> ListPets(int userId)
        {
            throw new NotImplementedException();
        }

        public int CountPets(int userId)
        {
            throw new NotImplementedException();
        }

        public Pet WritePet(NewPet pet)
        {
            throw new NotImplementedException();
        }

        public bool DeletePet(int petId)
        {
            throw new NotImplementedException();
        }

        private Pet GetPetFromReader(SqlDataReader reader)
        {
            Pet pet = new Pet()
            {
                PetId = Convert.ToInt32(reader["pet_id"]),
                PetName = Convert.ToString(reader["pet_name"]),
                PetType = reader["pet_type"].GetType() == typeof(DBNull) ? null : Convert.ToString(reader["pet_type"]),
                Brand = Convert.ToString(reader["brand"]),
                DateBirth = Convert.ToString(reader["date_birth"]),
                TimeBirth = reader["time_birth"].GetType() == typeof(DBNull) ? "12:00:00 AM" : Convert.ToString(reader["time_birth"]),
                DateDeath = reader["date_death"].GetType() == typeof(DBNull) ? null : Convert.ToString(reader["date_death"]),
                IsActive = Convert.ToBoolean(reader["is_active"]),
                IsHidden = Convert.ToBoolean(reader["is_hidden"]),
                UserId = Convert.ToInt32(reader["user_id"]),
            };

            return pet;
        }
    }
}

