<template>
  <v-menu
    ref="menu"
    v-model="isDatePickerOpened"
    :close-on-content-click="false"
    transition="scale-transition"
    offset-y
    min-width="290px">
    <template v-slot:activator="{ on, attrs }">
      <v-text-field
        v-model="dateFormatted"
        filled
        persistent-hint
        prepend-inner-icon="mdi-calendar"
        readonly
        v-bind="attrs"
        v-on="on"
        />
    </template>
    <v-date-picker
      v-model="date"
      no-title
      scrollable>
      <v-spacer></v-spacer>
      <v-btn
        text
        color="primary"
        @click="onDateSelected">
        OK
      </v-btn>
      <v-btn
        text
        color="error"
        @click="isDatePickerOpened = false">
        Cancel
      </v-btn>
    </v-date-picker>
  </v-menu>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import dates from '@/utils/dates'

@Component
export default class DatePickerComponent extends Vue {

    @Prop({ default: () => new Date() }) value: Date

    date = undefined as string
    isDatePickerOpened = false

    constructor() {
        super()
        console.log("this.value:", this.value)
        this.date = this.dateFormatted
    }

    get dateFormatted(): string {
        return dates.short(this.value)
    }

    onDateSelected() {
        console.log("this.value:", this.value);
        console.log("this.date:", this.date);
        this.date = this.dateFormatted
        const datePickerMenu = this.$refs.menu as any
        datePickerMenu.save(this.date)
        this.$emit('input', this.date)
        this.$forceUpdate()
    }
}
</script>
