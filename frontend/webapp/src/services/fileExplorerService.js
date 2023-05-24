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

    moveFile(move){
        return axios.put(`fileexplorer/move/file`, move)
    },

    deleteDirOrFile(formData){
        return axios.delete(`/fileexplorer/file-dir`,  {
            headers: {
                Accept: 'application/json',
                'Content-Type': 'multipart/form-data'
            },
            data: formData
        })
    }
    
}