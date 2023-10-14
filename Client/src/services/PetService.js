import axios from 'axios';

export default {

  getPet(pet) {
    return axios.get(`/pet/${pet.petId}`)
  },

  listPets(user) {
    return axios.get(`/${user.userId}`)
  },

  addPet(newPet) {
    return axios.post('/pet/add', newPet)
  },

  updatePet(pet) {
    return axios.put(`/pet/update/${pet.petId}`, pet)
  },

  deletePet(pet) {
    return axios.delete(`/pet/delete/${pet.petId}`)
  }

}