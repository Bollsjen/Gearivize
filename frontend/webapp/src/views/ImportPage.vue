<template>
  <b-container class="w-75 text-left mt-5">
    <b-row>
      <b-col sm="5" class="text-right">
        <svg style="width: 50%" fill="rgb(140,140,140)" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Free 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M64 256V160H224v96H64zm0 64H224v96H64V320zm224 96V320H448v96H288zM448 256H288V160H448v96zM64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V96c0-35.3-28.7-64-64-64H64z"/></svg>
      </b-col>

      <b-col sm="2" class="text-center d-flex justify-content-center">
        <svg style="width: 50%" fill="rgb(140,140,140)" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Free 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M502.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L402.7 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l370.7 0-73.4 73.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l128-128z"/></svg>
      </b-col>

      <b-col sm="5" class="text-left">
        <svg style="width: 60%" fill="rgb(140,140,140)" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"><!--! Font Awesome Free 6.4.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M256 64H384v64H256V64zM240 0c-26.5 0-48 21.5-48 48v96c0 26.5 21.5 48 48 48h48v32H32c-17.7 0-32 14.3-32 32s14.3 32 32 32h96v32H80c-26.5 0-48 21.5-48 48v96c0 26.5 21.5 48 48 48H240c26.5 0 48-21.5 48-48V368c0-26.5-21.5-48-48-48H192V288H448v32H400c-26.5 0-48 21.5-48 48v96c0 26.5 21.5 48 48 48H560c26.5 0 48-21.5 48-48V368c0-26.5-21.5-48-48-48H512V288h96c17.7 0 32-14.3 32-32s-14.3-32-32-32H352V192h48c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48H240zM96 448V384H224v64H96zm320-64H544v64H416V384z"/></svg>
      </b-col>

      <b-col sm="12" class="text-center mt-5">
        <b-button style="font-size: 20px" variant="primary" @click="importInstruments">
          Import instruments
        </b-button>
      </b-col>
    </b-row>
    <input type="file" style="display: none" ref="InstrumentImportInput" accept=".xlsx, .xls, .xlsb" @change="selectedFile" />
  </b-container>
</template>

<script>
import {importService} from "@/services/importService";

export default {
  components: {

  },
  props: {

  },
  data(){
    return {
    }
  },
  methods: {
    importInstruments(){
      this.$refs['InstrumentImportInput'].click()
    },
    selectedFile(e){
      let file = e.target.files[0]

      let formData = new FormData()
      formData.append('file', file)

      importService.importInstruments(formData)
          .then(result => console.log(result))
          .catch(error => console.log(error))
    }
  },
  computed: {

  }
}
</script>