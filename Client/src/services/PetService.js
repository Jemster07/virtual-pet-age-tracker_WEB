import axios from 'axios';

export default {

  getPet(pet) {
    return axios.get(`/pet/${pet.petId}`)
  },

  listPets(userId) {
    return axios.get(`/${userId}`)
  },

  addPet(newPet) {
    return axios.post('/pet/add', newPet)
  },

  updatePet(pet) {
    return axios.put(`/pet/update/${pet.petId}`, pet)
  },

  deactivatePet(pet) {
    return axios.delete(`/pet/deactivate/${pet.petId}`)
  },

  deletePets() {
    return axios.delete('delete/pets')
  },

}