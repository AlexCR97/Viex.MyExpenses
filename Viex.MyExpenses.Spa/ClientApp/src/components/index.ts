import vue from 'vue'
import ConfirmDialog from './ConfirmDialog'
import DatePager from './DatePager.vue'
import DatePicker from './DatePicker.vue'
import LoadingDialog from './LoadingDialog'
import PageHeader from './PageHeader'
import SideNav from './SideNav'
import TransactionList, { TransactionItem } from './TransactionList'

vue.component('ConfirmDialog', ConfirmDialog)
vue.component('DatePager', DatePager)
vue.component('DatePicker', DatePicker)
vue.component('LoadingDialog', LoadingDialog)
vue.component('PageHeader', PageHeader)
vue.component('SideNav', SideNav)
vue.component('TransactionList', TransactionList)
vue.component('TransactionItem', TransactionItem)