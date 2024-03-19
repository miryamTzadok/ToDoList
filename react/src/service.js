// import axios from 'axios';
import axios from './axiosConfig.ts';
axios.defaults.baseURL = 'http://localhost:5288/';
export default {
  getTasks: async () => {
    const result = await axios.get("allitems")
    return result.data;
  },
  addTask: async(name)=>{
    await axios.post(`newitem/${name}`)
    return {};
  },
  setCompleted: async(id, isComplete)=>{
    await axios.put(`item/${id}/${isComplete}`) 
  },

  deleteTask:async(id)=>{
     await axios.delete(`de/${id}`) 
  }
};
