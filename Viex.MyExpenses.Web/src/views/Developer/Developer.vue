<template>
    <v-container>
        
        <PageHeader icon="mdi-code-tags" title="Developer" class="mb-8"/>

        <div class="mb-6">
            <p class="ma-0 mb-2">Transactions</p>
            <v-btn color="primary" class="text-capitalize mr-4" @click="onSeedTransactionsClicked">Seed</v-btn>
            <v-btn color="error" class="text-capitalize" @click="onDropTransactionsClicked">Drop</v-btn>
        </div>

        <div class="mb-6">
            <p class="ma-0 mb-2">CategoryDescriptors</p>
            <v-btn color="primary" class="text-capitalize mr-4" @click="onSeedCategoriesClicked">Seed</v-btn>
            <v-btn color="error" class="text-capitalize" @click="onDropCategoriesClicked">Drop</v-btn>
        </div>

        <div class="mb-6">
            <p class="ma-0 mb-2">TransactionTypeDescriptors</p>
            <v-btn color="primary" class="text-capitalize mr-4" @click="onSeedTransactionTypesClicked">Seed</v-btn>
            <v-btn color="error" class="text-capitalize" @click="onDropTransactionTypesClicked">Drop</v-btn>
        </div>

    </v-container>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import files from '@/utils/files'
import api from "@/api"

@Component
export default class DeveloperComponent extends Vue {

    async onDropCategoriesClicked() {

    }

    async onDropTransactionsClicked() {
        try {
            // TODO Add some sort of loading animation
            await api.transactions.deleteAll();
        } catch {
            // TODO Handle error
        } finally {
            // TODO Stop loading animtions
        }
    }

    async onDropTransactionTypesClicked() {
        
    }

    async onSeedCategoriesClicked() {
        const selectedFiles = await files.triggerFileInputClick({ accept: '.json' })

        if (selectedFiles.length == 0)
            return
        
        const file = selectedFiles.item(0)
        const json = await files.getJsonContents<string[]>(file)

        console.log("Seeding...");
        await api.descriptors.createCategories(json);
        console.log("Seeded!");
    }

    async onSeedTransactionsClicked() {
        const selectedFiles = await files.triggerFileInputClick({ accept: '.csv' })

        if (selectedFiles.length == 0)
            return

        const csvFile = selectedFiles.item(0)
        const csvFileBase64 = await files.toBase64(csvFile)

        try {
            // TODO Add some sort of loading animation
            await api.transactions.importFromCsv(csvFileBase64)
        } catch {
            // TODO Handle error
        } finally {
            // TODO Stop loading animtions
        }
    }

    async onSeedTransactionTypesClicked() {
        const selectedFiles = await files.triggerFileInputClick({ accept: '.json' })

        if (selectedFiles.length == 0)
            return
        
        const file = selectedFiles.item(0)
        const json = await files.getJsonContents<string[]>(file)

        console.log("Seeding...");
        await api.descriptors.createTransactionTypes(json);
        console.log("Seeded!");
    }
}
</script>
