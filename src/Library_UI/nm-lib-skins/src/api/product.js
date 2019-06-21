const get = () => {
  return new Promise(resolve => {
    resolve({
      id: 1,
      title: '三星Galaxy Node 10',
      price: 6999,
      store: 98989,
      des: `三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy
         Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10
         三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10
         三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10
         三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10
         三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10三星Galaxy Node 10`
    })
  })
}

export default { get }
