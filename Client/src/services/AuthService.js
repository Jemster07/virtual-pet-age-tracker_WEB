import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },

  deactivateUser(user) {
    return axios.delete(`/deactivate/${user.username}`)
  },
  
  deleteUsers() {
    return axios.delete('/delete/users')
  },

}