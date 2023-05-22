import axios from '@/utils/axios'

export const fileExplorerService = {
    
    getAllFiles(){
      return axios.get(`/fileexplorer`)
    },
    
}