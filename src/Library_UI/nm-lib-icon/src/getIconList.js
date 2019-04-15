//从demo_index.html文件中获取图标的列表
var list = [];
document.querySelectorAll(".content.symbol>ul>li").forEach(item => {
  var name = item.querySelector(".name").innerText;
  var code = item.querySelector(".code-name").innerText;
  code = code.substring(6, code.length);
  list.push({ name, code });
});
console.log(JSON.stringify(list));
