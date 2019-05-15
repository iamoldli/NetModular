import Vue from 'vue'
import lib from '../../packages/library'
import Tmpl from './tmpl'
import CodePreview from './code-preview'

Vue.component(`${lib.prefix}Tmpl`, Tmpl)
Vue.component(`${lib.prefix}CodePreview`, CodePreview)
