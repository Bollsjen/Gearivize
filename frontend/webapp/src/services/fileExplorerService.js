import axios from '@/utils/axios'

export const fileExplorerService = {
    
    getAllFiles(){
      return axios.get(`/fileexplorer`)
    },

    uploadFile(formData){
        return axios.post(`/fileexplorer/file`, formData, {
            headers: {
                Accept: 'application/json',
                'Content-Type': 'multipart/form-data'
            }
        })
    }
    
}