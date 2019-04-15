import { on, off } from 'nm-lib-utils/src/utils/dom'
var resizableHandleList = new Map()
const typeMap = {
  'left-top': 'se',
  top: 's',
  'right-top': 'sw',
  left: 'e',
  right: 'w',
  'left-bottom': 'ne',
  bottom: 'n',
  'right-bottom': 'nw'
}
let parseDom = arg => {
  var objE = document.createElement('div')
  objE.innerHTML = arg
  return objE.childNodes[0]
}
let ResizableHandle = function(selfNode, bindNode, limitNode, type) {
  let limitWidth = ['e', 'w', 'se', 'sw', 'nw', 'ne'] // 允许宽度类型限制
  let limitHeight = ['n', 's', 'se', 'sw', 'nw', 'ne'] // 允许高度类型限制
  let flg = false
  let data = {
    selfNode: selfNode,
    bindNode: bindNode,
    limitNode: limitNode,
    type: type,
    enabled: true,
    minWidth: 200,
    minHeight: 100
  }
  let mousePoint = {
    startX: 0,
    startY: 0,
    endX: 0,
    endY: 0
  }
  // 限制区域
  let limitArea = {
    right: 1024,
    bottom: 900,
    left: 0,
    top: 0
  }

  let selfArea = {
    right: 0,
    bottom: 0,
    left: 0,
    top: 0,
    width: 0,
    height: 0
  }
  let configArea = () => {
    let self = data.selfNode.getBoundingClientRect()
    selfArea.left = self.left
    selfArea.top = self.top
    selfArea.right = self.width + self.left
    selfArea.bottom = self.top + self.height
    selfArea.width = self.width
    selfArea.height = self.height
    let limit = data.limitNode
      ? data.limitNode.getBoundingClientRect()
      : window.document.body.getBoundingClientRect()
    limitArea.left = limit.left
    limitArea.top = limit.top
    limitArea.right = limit.width + limit.left
    limitArea.bottom = limit.top + limit.height
  }

  let mousedown = ev => {
    if (!data.enabled || ev.button === 2) return
    let x = ev.pageX
    let y = ev.pageY
    mousePoint.startX = x
    mousePoint.startY = y
    // 设置范围
    configArea()
    data.selfNode.classList.add('nm-resizable-move')
    on(window.document, 'mousemove', mousemove)
    on(window.document, 'mouseup', mouseup)
    window.document.body.style.userSelect = 'none'
    flg = true
  }
  let mousemove = ev => {
    if (flg) return
    mousePoint.endX = Math.min(
      Math.max(limitArea.left, ev.pageX),
      limitArea.right
    )
    mousePoint.endY = Math.min(
      Math.max(limitArea.top, ev.pageY),
      limitArea.bottom
    )
    if (limitHeight.includes(type)) {
      let setY = ['n', 'ne', 'nw'].includes(type)
        ? mousePoint.startY - mousePoint.endY
        : mousePoint.endY - mousePoint.startY
      setY = Math.max(data.minHeight, selfArea.height - setY)
      data.selfNode.style.height = setY + 'px'
      if (['s', 'sw', 'se'].includes(type)) {
        data.selfNode.style.top = selfArea.bottom - setY + 'px'
      }
    }
    if (limitWidth.includes(type)) {
      // 设置宽度
      let setX = ['w', 'sw', 'nw'].includes(type)
        ? mousePoint.startX - mousePoint.endX
        : mousePoint.endX - mousePoint.startX
      setX = Math.max(data.minWidth, selfArea.width - setX)
      data.selfNode.style.width = setX + 'px'
      if (['e', 'se', 'ne'].includes(type)) {
        data.selfNode.style.left = selfArea.right - setX + 'px'
      }
    }
  }
  let mouseup = () => {
    window.document.body.style.userSelect = null
    flg = false
    data.selfNode.classList.remove('nm-resizable-move')
    off(window.document, 'mousemove', mousemove)
    off(window.document, 'mouseup', mouseup)
  }
  return {
    mousedown: mousedown,
    data: data
  }
}
// 产生随机数函数
let RndNum = n => {
  var rnd = 'resizable_'
  for (let i = 0; i < n; i++) {
    rnd += Math.floor(Math.random() * 10)
  }
  return rnd
}
// 取ID 没有则添加ID
let getId = el => {
  let id = ''
  if (!el.getAttribute('resizable-node')) {
    id = RndNum(6)
    el.setAttribute('resizable-node', id)
  } else {
    id = el.getAttribute('resizable-node')
  }
  return id
}
const Resizable = {
  inserted: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    if (binding.value.enabled) {
      let parentNode = binding.value.parentNode
        ? el.querySelector(binding.value.parentNode)
        : el
      let id = getId(el)
      let resizableDirectionMap = {}
      let limitNode = null
      if (
        binding.value.limitNode != null &&
        binding.value.limitNode.length > 0
      ) {
        limitNode = window.document.querySelector(binding.value.limitNode)
      }
      binding.value.resizableDirection.forEach(o => {
        let type = typeMap[o.toLowerCase()]
        if (type) {
          var div = binding.value.customElem
            ? parseDom(binding.value.customElem)
            : document.createElement('div')
          div.classList.add('nm-resizable')
          div.classList.add('nm-resizable-' + type)
          let p = ResizableHandle(el, div, limitNode, type)
          resizableDirectionMap[type] = { handle: p, elem: div }
          on(div, 'mousedown', p.mousedown)
          parentNode.appendChild(div)
        }
      })
      resizableHandleList.set(id, resizableDirectionMap)
    }
  },
  componentUpdated: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    // if (binding.oldValue !== binding.value) {
    let id = getId(el)
    let p = resizableHandleList.get(id)
    if (!p) return
    let limitNode = null
    if (binding.value.limitNode != null && binding.value.limitNode.length > 0) {
      limitNode = window.document.querySelector(binding.value.limitNode)
    }

    Object.keys(p).forEach(o => {
      if (binding.value.enabled) {
        p[o]['elem'].classList.add('nm-resizable')
        p[o]['elem'].classList.add('nm-resizable-' + o)
      } else {
        p[o]['elem'].classList.remove('nm-resizable')
        p[o]['elem'].classList.remove('nm-resizable-' + o)
      }
      p[o]['handle'].data.enabled = binding.value.enabled
      p[o]['handle'].data.limitNode = limitNode
    })
    // }
  },
  unbind: (el, binding) => {
    if (binding.value == null || binding.value === '') return
    if (binding.value.enabled) {
      let id = getId(el)
      let p = resizableHandleList.get(id)
      if (p != null) {
        Object.keys(p).forEach(o => {
          off(p[o]['elem'], 'mousedown', p[o]['handle'].mousedown)
        })
      }
    }
  }
}
export default {
  name: 'Resizable',
  directive: Resizable
}
