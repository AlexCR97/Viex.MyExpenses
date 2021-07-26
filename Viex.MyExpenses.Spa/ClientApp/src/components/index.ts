import vue from 'vue'
import DatePager from './DatePager.vue'
import DatePicker from './DatePicker.vue'
import PageHeader from './PageHeader'
import SideNav from './SideNav'
import TransactionList, { TransactionItem } from './TransactionList'

vue.component('DatePager', DatePager)
vue.component('DatePicker', DatePicker)
vue.component('PageHeader', PageHeader)
vue.component('SideNav', SideNav)
vue.component('TransactionList', TransactionList)
vue.component('TransactionItem', TransactionItem)