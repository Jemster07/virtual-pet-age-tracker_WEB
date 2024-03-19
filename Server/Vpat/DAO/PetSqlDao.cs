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
            Pet pet = new Pet();
            string sql = "SELECT pet_id, pet_name, pet_type, brand, birthday, " +
                "date_death, is_hidden, user_id FROM pets WHERE pet_id = @petId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
            List<Pet> petList = new List<Pet>();
            string sql = "SELECT pet_id, pet_name, pet_type, brand, birthday, " +
                "date_death, is_hidden, user_id FROM pets WHERE user_id = @userId AND is_hidden = 0 " +
                "ORDER BY pet_name;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Pet fetchedPet = GetPetFromReader(reader);

                        petList.Add(fetchedPet);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return petList;
        }

        public Pet WritePet(Pet Pet)
        {
            int petId = 0;
            string sql = "INSERT INTO pets (pet_name, pet_type, brand, birthday, " +
                "is_hidden, user_id) OUTPUT INSERTED.pet_id VALUES (@petName, @petType, " +
                "@brand, @birthday, @isHidden, @userId);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@petName", Pet.PetName);
                    cmd.Parameters.AddWithValue("@petType", Pet.PetType);
                    cmd.Parameters.AddWithValue("@brand", Pet.Brand);
                    cmd.Parameters.AddWithValue("@birthday", Pet.Birthday);
                    cmd.Parameters.AddWithValue("@isHidden", false);
                    cmd.Parameters.AddWithValue("@userId", Pet.UserId);

                    petId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return ReadPet(petId);
        }

        public Pet UpdatePet(Pet pet)
        {
            string sql = "";

            if (pet.DateDeath != null)
            {
                sql = "UPDATE pets SET pet_name = @petName, pet_type = @petType, brand = @brand, " +
                "birthday = @birthday, date_death = @dateDeath " +
                "WHERE pet_id = @petId;";
            }
            else // pet.DateDeath == null
            {
                sql = "UPDATE pets SET pet_name = @petName, pet_type = @petType, brand = @brand, " +
                "birthday = @birthday WHERE pet_id = @petId;";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@petName", pet.PetName);
                    cmd.Parameters.AddWithValue("@petType", pet.PetType);
                    cmd.Parameters.AddWithValue("@brand", pet.Brand);
                    cmd.Parameters.AddWithValue("@birthday", pet.Birthday);

                    if (pet.DateDeath != null)
                    {
                        cmd.Parameters.AddWithValue("@dateDeath", pet.DateDeath);
                    }

                    cmd.Parameters.AddWithValue("@petId", pet.PetId);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows != 1)
                    {
                        throw new Exception("Error updating pet information.");
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return ReadPet(pet.PetId);
        }

        public bool DeactivatePet(int petId)
        {
            bool petDeleted = true;
            string sql = "UPDATE pets SET is_hidden = 1 WHERE pet_id = @petId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@petId", petId);
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows != 1)
                    {
                        petDeleted = false;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return petDeleted;
        }

        public bool DeletePets()
        {
            bool petsDeleted = true;
            string sql = "DELETE FROM pets WHERE is_hidden = 1;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows == 0)
                    {
                        petsDeleted = false;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return petsDeleted;
        }

        private Pet GetPetFromReader(SqlDataReader reader)
        {
            Pet pet = new Pet()
            {
                PetId = Convert.ToInt32(reader["pet_id"]),
                PetName = Convert.ToString(reader["pet_name"]),
                PetType = Convert.ToString(reader["pet_type"]),
                Brand = Convert.ToString(reader["brand"]),
                Birthday = Convert.ToDateTime(reader["birthday"]),
                DateDeath = reader["date_death"].GetType() == typeof(DBNull) ? null : Convert.ToDateTime(reader["date_death"]),
                IsHidden = Convert.ToBoolean(reader["is_hidden"]),
                UserId = Convert.ToInt32(reader["user_id"]),
            };

            return pet;
        }
    }
}