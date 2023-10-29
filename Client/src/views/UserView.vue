<template>
  <div id="user">
    <table class="table is-striped is-hoverable" v-if="petList != ''">
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
          <td>{{ pet.birthday }}</td>
          <td>{{ pet.dateDeath }}</td>
          <td>{{ pet.age }}</td>
          <td>
            <div class="buttons are-small">
              <button class="button is-info" v-on:click="editClicked(pet)">Edit</button>
              <button class="button is-info">R.I.P</button>
              <button class="button is-danger is-light">Delete</button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <div id="edit-modal" class="modal is-clipped">
      <div class="modal-background"></div>
      <div class="modal-content">
        <div class="box">
          <label class="label is-size-4">Edit {{ activePet.petName }}</label>

          <div class="field">
            <label class="label">Name</label>
              <div class="control">
                <input class="input" type="text" v-model="editPet.petName">
              </div> 
              <p class="help">Current Value: {{ activePet.petName }}</p>    
          </div>
          <div class="field">
            <label class="label">Brand/Species</label>
              <div class="control">
                <input class="input" type="text" v-model="editPet.brand">
              </div>
              <p class="help">Current Value: {{ activePet.brand }}</p>    
          </div>
          <div class="field">
            <label class="label">Type</label>
              <div class="control">
                <input class="input" type="text" v-model="editPet.petType">
              </div>
              <p class="help">Current Value: {{ activePet.petType }}</p>    
          </div>
          <div class="field">
            <label class="label">Day of Birth</label>
              <div class="control">
                <input class="input" type="text" v-model="editPet.dateBirth">
              </div>
              <p class="help">Current Value: {{ activePet.dateBirth }}</p>    
          </div>
          <div class="field">
            <label class="label">Time of Birth</label>
              <div class="control">
                <input class="input" type="text" v-model="editPet.timeBirth">
              </div>
              <p class="help">Current Value: {{ activePet.timeBirth }}</p>    
          </div>
          <div class="field is-grouped is-grouped-right">
            <p class="control">
              <a class="button is-info">Submit</a>
            </p>
            <p class="control">
              <a class="button is-light" v-on:click="cancelForm()">Cancel</a>
            </p>
          </div>

        </div>
      </div>
      <button class="modal-close is-large" aria-label="close" v-on:click="cancelForm()"></button>
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
      newPet: {
        petName: "",
        petType: "",
        brand: "",
        dateBirth: "",
        timeBirth: "",
        userId: this.$store.state.user.userId,
      },
      editPet: {
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
      activePet: {
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
      let response = await PetService.listPets(this.currentUserId);

      if (response == null) {
        console.log("There was an error loading your pet list.");
      } else {
        this.petList = response.data;
      }
    },

    openModal() {
      const editModal = document.querySelector('#edit-modal');
      const list = editModal.classList;
      list.add("is-active");
    },
    closeModal() {
      const editModal = document.querySelector('#edit-modal');
      const list = editModal.classList;
      list.remove("is-active");
    },
    editClicked(pet) {
      this.mapActivePet(pet);
      this.mapEditPet(pet);
      this.openModal();
    },
    cancelForm() {
      this.activePet = {};
      this.editPet = {};
      this.newPet = {};
      this.closeModal();
    },
    submitForm() {
      alert("Did it work?");
    },
    mapActivePet(pet) {
      this.activePet.petId = pet.petId;
      this.activePet.petName = pet.petName;
      this.activePet.petType = pet.petType;
      this.activePet.brand = pet.brand;
      this.activePet.dateBirth = pet.dateBirth;
      this.activePet.timeBirth = pet.timeBirth;
      this.activePet.dateDeath = pet.dateDeath;
    },
    mapEditPet(pet) {
      this.editPet.petId = pet.petId;
      this.editPet.petName = pet.petName;
      this.editPet.petType = pet.petType;
      this.editPet.brand = pet.brand;
      this.editPet.dateBirth = pet.dateBirth;
      this.editPet.timeBirth = pet.timeBirth;
      this.editPet.dateDeath = pet.dateDeath;
    },
  },
};
</script>