const get = () => {
  return new Promise(resolve => {
    resolve({
      id: 1,
      title: '三星Galaxy Node 10',
      price: 6999,
      store: 98989
    })
  })
}

export { get }
