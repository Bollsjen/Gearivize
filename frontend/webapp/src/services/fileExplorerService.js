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
    },

    deleteDirOrFile(formData){
        return axios.delete(`/fileexplorer/file-dir`, formData, {
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'multipart/form-data'
                }
            })
    }
    
}