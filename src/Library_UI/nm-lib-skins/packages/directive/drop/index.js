import { on, off } from 'nm-lib-utils/src/utils/dom'
var dropHandleList = new Map()
let DropHandle = function(selfNode, bindNode, limitNode, enabled) {
  let data = {
    selfNode: selfNode, // 改变节点
    bindNode: bindNode, // 绑定拖动节点
    limitNode: limitNode || null, // 限制节点
    enabled: enabled || true // 是否启用拖动
  }
  // 鼠标偏移补偿
  let offsetX = 0
  let offsetY = 0
  let flg = false
  // 限制区域
  let limitArea = {
    right: 0,
    bottom: 0,
    left: 0,
    top: 0
  }
  // 设置拖动限制区域
  let configLimitArea = function(ev) {
    let obj = data.limitNode ? data.limitNode.getBoundingClientRect() : null
    // 设置限制区域
    limitArea.right = obj
      ? obj.width - selfNode.clientWidth + obj.left
      : 9999999
    limitArea.bottom = obj
      ? obj.height - selfNode.clientHeight + obj.top
      : 9999999
    limitArea.left = obj ? obj.left : -9999999
    limitArea.top = obj ? obj.top : -9999999
    // 这偏移位子
    offsetX = ev.offsetX + parseInt(bindNode.offsetLeft)
    offsetY = ev.offsetY + parseInt(bindNode.offsetTop)
  }
  let mousemove = function(ev) {
    if (!flg) return
    let x = ev.pageX
    let y = ev.pageY
    let setX = x - offsetX
    let setY = y - offsetY
    setX = Math.min(Math.max(setX, limitArea.left), limitArea.right)
    setY = Math.min(Math.max(setY, limitArea.top), limitArea.bottom)
    selfNode.style.left = setX + 'px'
    selfNode.style.top = setY + 'px'
  }
  let mouseup = function(ev) {
    flg = false
    window.document.body.style.userSelect = null
    off(window.document, 'mousemove', mousemove)
    off(window.document, 'mouseup', mouseup)
    selfNode.classList.remove('nm-resizable-move')
  }
  let mousedown = function(ev) {
    if (!data.enabled || ev.button === 2) return
    window.document.body.style.userSelect = 'none'
    configLimitArea(ev)
    on(window.document, 'mousemove', mousemove)
    on(window.document, 'mouseup', mouseup)
    flg = true
    selfNode.classList.add('nm-resizable-move')
  }
  return {
    mousedown: mousedown,
    data: data
  }
}
// 产生随机数函数
let RndNum = n => {
  let rnd = 'drop_'
  for (let i = 0; i < n; i++) {
    rnd += Math.floor(Math.random() * 10)
  }
  return rnd
}
// 取ID 没有则添加ID
let getId = el => {
  let Id = ''
  if (!el.getAttribute('drop-node')) {
    Id = RndNum(6)
    el.setAttribute('drop-node', Id)
  } else {
    Id = el.getAttribute('drop-node')
  }
  return Id
}
const drop = {
  inserted: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    if (binding.value.enabled) {
      let dropNode = el.querySelector(binding.value.dropNode)
      dropNode.classList.add('is-drop')
      let id = getId(dropNode)
      let limitNode = null
      if (
        binding.value.limitNode != null &&
        binding.value.limitNode.length > 0
      ) {
        limitNode = window.document.querySelector(binding.value.limitNode)
      }
      let dropHandle = new DropHandle(
        el,
        dropNode,
        limitNode,
        binding.value.enabled
      )
      dropHandleList.set(id, dropHandle)
      on(dropNode, 'mousedown', dropHandle.mousedown)
    }
  },
  componentUpdated: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    // if (binding.oldValue !== binding.value) {
    let dropNode = el.querySelector(binding.value.dropNode)
    let id = getId(dropNode)
    let p = dropHandleList.get(id)
    if (p == null) return
    let limitNode = null
    if (binding.value.limitNode != null && binding.value.limitNode.length > 0) {
      limitNode = window.document.querySelector(binding.value.limitNode)
    }
    p.data.limitNode = limitNode
    p.data.enabled = binding.value.enabled
    if (!binding.value.enabled) {
      dropNode.classList.remove('is-drop')
    } else {
      dropNode.classList.add('is-drop')
    }
    // }
  },
  unbind: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    let dropNode = el.querySelector(binding.value.dropNode)
    let id = getId(dropNode)
    let p = dropHandleList.get(id)
    if (p != null) {
      off(dropNode, 'mousedown', p.mousedown)
      dropHandleList.delete(id)
    }
  }
}

export default {
  name: 'Drop',
  directive: drop
}
