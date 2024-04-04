<template>
  <div id="user">
      <div class="columns is-centered">
        <div class="column is-narrow">

          <div class="columns">
            <div class="column">
              <h1 class="title has-text-centered">{{ $store.state.user.username }}'s pets</h1>
            </div>
          </div>

          <div class="columns is-vcentered">
            <div class="column">
                <hr class="solid">
            </div>
            <div class="column is-narrow">
              <div class="container">
                <button class="button is-success is-pulled-right" v-on:click="addClicked()">Add New Pet</button>
              </div>
            </div>
          </div>

          <div class="columns">
            <div class="column">
              <div class="container box">            
                <table class="table is-hoverable" v-if="petList != ''">
                  <thead>
                    <tr>
                      <th>Name</th>
                      <th>Brand/Species</th>
                      <th>Type</th>
                      <th>Birthday</th>
                      <th>Date of Death</th>
                      <th>Age</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="pet in petList" :key="pet.petId">
                      <td>{{ pet.petName }}</td>
                      <td>{{ pet.brand }}</td>
                      <td>{{ pet.petType }}</td>
                      <td>{{ formattedBirthday(pet) }}</td>
                      <td>{{ pet.dateDeath }}</td>
                      <td>{{ pet.age }}</td>
                      <td>
                        <div class="buttons are-small">
                          <button class="button is-info" v-on:click="editClicked(pet)">Edit</button>
                          <button class="button is-info" v-if="pet.dateDeath == null" v-on:click="ripClicked(pet)">R.I.P</button>
                          <button class="button is-danger is-light">Delete</button>
                        </div>
                      </td>
                    </tr>
                  </tbody>
                </table> 
              </div>
            </div>
          </div>

        </div>
      </div>     

    <div id="alert-modal" class="modal">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="box">
          &nbsp;
          <p class="label is-size-4 has-text-centered">{{ alertMessage }}</p>
          &nbsp;
          <div class="buttons is-centered">
            <p class="control">
              <a class="button is-info" v-on:click="closeAlert()">Okay</a>
            </p>
          </div>        
        </div>
      </div>
      <button class="modal-close is-large" aria-label="close" v-on:click="closeAlert()"></button>
    </div>

    <div id="form-modal" class="modal is-clipped">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="box">
          <label class="label is-size-4 has-text-centered">{{ formTitle }}</label>

          <form v-on:submit.prevent="submitForm">
            <div v-if="buttonTrigger != 'ripPet'">
              <div class="field">
                <label class="label">Name</label>
                <div class="control">
                  <input class="input" type="text" maxLength="50" v-model="viewPet.petName" required>
                </div> 
                <p class="help" v-if="buttonTrigger == 'editPet'">Previous Value: {{ selectedPet.petName }}</p>    
              </div>
              <div class="field">
                <label class="label">Brand/Species</label>
                <div class="control">
                  <input class="input" type="text" maxLength="50" v-model="viewPet.brand" required>
                </div>
                <p class="help" v-if="buttonTrigger == 'editPet'">Previous Value: {{ selectedPet.brand }}</p>    
              </div>
              <div class="field">
                <label class="label">Type</label>
                <div class="control">
                  <input class="input" type="text" maxLength="50" v-model="viewPet.petType" required>
                </div>
                <p class="help" v-if="buttonTrigger == 'editPet'">Previous Value: {{ selectedPet.petType }}</p>    
              </div>
              <div class="field">
                <label class="label">Day of Birth</label>
                <div class="control">
                  <input class="input" type="date" v-model="viewPet.dateBirth" required>
                </div>
                <p class="help" v-if="buttonTrigger == 'editPet'">Previous Value: {{ selectedPet.dateBirth }}</p>    
              </div>
              <div class="field">
                <label class="label">Time of Birth</label>
                <div class="control">
                  <input class="input" type="time" id="form-timeBirth" v-model="viewPet.timeBirth" required>                     
                </div>
                <p class="help" v-if="buttonTrigger == 'editPet'">Previous Value: {{ selectedPet.timeBirth }}</p>
                <br>
                <label class="checkbox">
                  <input type="checkbox" id="formCheckbox" name="formCheckboxClicked" v-on:change="formCheckboxClicked()"> Use current time 
                </label>    
              </div>
            </div>
            <div class="field is-grouped is-grouped-right">
              <p class="control">
                <button type="submit" class="button is-info">Submit</button>
              </p>
              <p class="control">
                <a class="button is-light" v-on:click="closeForm()">Cancel</a>
              </p>
            </div>    
          </form>

        </div>
      </div>
      <button class="modal-close is-large" aria-label="close" v-on:click="closeForm()"></button>
    </div>
  </div>
</template>

<script>
import PetService from "../services/PetService.js";

export default {
  data() {
    return {
      currentUserId: this.$store.state.user.userId,
      petList: [],
      alertMessage: "",
      buttonTrigger: "",
      formTitle: "",

      viewPet: {
        petId: 0,
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: "",
        dateDeath: "",
        isHidden: false,
        userId: this.$store.state.user.userId,
      },
      selectedPet: {
        petId: 0,
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: "",
        dateDeath: "",
        isHidden: false,
        userId: this.$store.state.user.userId,
      },
    };
  },

  async mounted() {
    this.listPets();
  },

  methods: {
    async listPets() {
      await PetService.listPets(this.currentUserId).then(response => {
        if (response) {
          this.petList = response.data;
        } else {
          console.log("There was an error loading your pet list.");
        }
      })
      .catch(error => {
        console.log(error);
      })
    },
    async updatePet() {
      await PetService.updatePet(this.viewPet).then(response => {
        if (response) {
          this.alertMessage = `${this.viewPet.petName} successfully updated.`;
          this.closeForm();
          this.listPets();
          this.openAlert();
        } else {
          this.alertMessage = `There was an error updating ${this.selectedPet.petName}.`;
          this.openAlert();
        }
      })
      .catch(error => {
        console.log(error);
      })
    },
    async addPet() {
      await PetService.addPet(this.viewPet).then(response => {
        if (response) {
          this.alertMessage = `${this.viewPet.petName} successfully added`;
          this.closeForm();
          this.listPets();
          this.openAlert();
        } else {
          this.alertMessage = `There was an error adding ${this.viewPet.petName}.`;
          this.openAlert();
        }
      })
      .catch(error => {
        console.log(error);
      })
    },
    async deactivatePet() {
      await PetService.deactivatePet(this.viewPet).then(response => {
        if (response) {
          this.alertMessage = `Rest in peace, ${this.viewPet.petName}!`;
        } else {
          this.alertMessage = `There was an error recording ${this.selectedPet.petName}'s death.`;
        }
      })
      .catch(error => {
        console.log(error);
      })
    },


    openAlert() {
      const alertModal = document.querySelector('#alert-modal');
      const list = alertModal.classList;
      list.add("is-active");
    },
    closeAlert() {     
      const alertModal = document.querySelector('#alert-modal');
      const list = alertModal.classList;
      list.remove("is-active");
      this.alertMessage = "";
    },


    addClicked() {
      this.buttonTrigger = "addPet";
      this.formTitle = "Add a New Pet";
      this.openFormModal();
    },
    editClicked(pet) {
      this.buttonTrigger = "editPet";
      this.formTitle = `Edit ${pet.petName}`;
      this.mapSelectedPet(pet);
      this.mapViewPet(pet);
      this.openFormModal();
    },
    ripClicked(pet) {
      this.buttonTrigger = "ripPet";
      this.formTitle = `Record ${pet.petName}'s Death`;
      this.mapViewPet(pet);
      this.openFormModal();
    },


    openFormModal() {
      const formModal = document.querySelector('#form-modal');
      const list = formModal.classList;
      list.add("is-active");
    },
    closeFormModal() {
      const formModal = document.querySelector('#form-modal');
      const list = formModal.classList;
      list.remove("is-active");
    },
    closeForm() {
      if (this.buttonTrigger != "ripPet") {
        var chkBox = document.getElementById('formCheckbox');
        chkBox.checked = false;
      
        const timeInput = document.querySelector("#form-timeBirth");
        timeInput.removeAttribute("readonly", "");
      
        this.selectedPet = {};
      }
      
      this.viewPet = {};
      this.buttonTrigger = "";
      this.formTitle = "";
      this.closeFormModal();
    },
    formCheckboxClicked() {
      var chkBox = document.getElementById('formCheckbox');
      const timeInput = document.querySelector("#form-timeBirth");

      if (chkBox.checked) {        
        const dateTime = Date.now();
        const options = {
          hour: "2-digit",
          minute: "2-digit",
        };             
        const currentTime = new Date(dateTime).toLocaleString("en-GB", options);
        this.viewPet.timeBirth = currentTime;

        timeInput.setAttribute("readonly", "");
      }
      else {
        timeInput.removeAttribute("readonly", "");
      }
    },
    submitForm() {
      if (this.buttonTrigger === "addPet") {
				this.addPet();
			}
			else if (this.buttonTrigger === "editPet") 
			{
				this.updatePet();
			}
      else // this.buttonTrigger === "ripPet"
      {
        this.deactivatePet();
      }
    },


    mapSelectedPet(pet) {
      this.selectedPet.petId = pet.petId;
      this.selectedPet.petName = pet.petName;
      this.selectedPet.petType = pet.petType;
      this.selectedPet.brand = pet.brand;
      this.selectedPet.dateBirth = this.formattedDateBirth(pet);
      this.selectedPet.timeBirth = this.formattedTimeBirth(pet);
      this.selectedPet.dateDeath = pet.dateDeath;
    },
    mapViewPet(pet) {
      this.viewPet.petId = pet.petId;
      this.viewPet.petName = pet.petName;
      this.viewPet.petType = pet.petType;
      this.viewPet.brand = pet.brand;
      this.viewPet.dateBirth = pet.dateBirth;
      this.viewPet.timeBirth = pet.timeBirth;
      this.viewPet.dateDeath = pet.dateDeath;
    },


    formattedBirthday(pet) {     
      const dateTime = pet.birthday;
      const options = {
        dateStyle: 'medium',
        timeStyle: 'short',
      };     
      return new Date(dateTime).toLocaleString(undefined, options);
    },
    formattedDateBirth(pet) {     
      const dateTime = pet.birthday;
      const options = {
        dateStyle: 'medium',
      };     
      return new Date(dateTime).toLocaleString(undefined, options);
    },
    formattedTimeBirth(pet) {     
      const dateTime = pet.birthday;
      const options = {
        timeStyle: 'short',
      };     
      return new Date(dateTime).toLocaleString(undefined, options);
    },
  },
};
</script>

<style scoped>
  hr.solid {
    border-top: 2px solid lightgrey;
  }
</style>