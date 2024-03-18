import ProductList from "./ProductList";
import {useEffect} from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import {useAppDispatch, useAppSelector} from "../../store/configureStore";
import {fetchFilters, fetchProductsAsync, productSelectors, setPageNumber, setProductParams} from "./catalogSlice";
import {
    Grid,
    Paper,
} from "@mui/material";
import ProductSearch from "./ProductSearch";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import CheckBoxButton from "../../app/components/CheckBoxButton";
import AppPagination from "../../app/components/AppPagination";

const sortOption = [
    {value: 'name', label: 'Alphabetical'},
    {value: 'priceDecs', label: 'Price - Hight to low'},
    {value: 'price', label: 'Price - Low to hight'},
]

export default function Catalog() {
    const products = useAppSelector(productSelectors.selectAll)
    const {
        productsLoaded,
        filtersLoaded,
        brands,
        types,
        productParams,
        metaData
    } = useAppSelector(state => state.catalog)
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!productsLoaded) dispatch(fetchProductsAsync());
    }, [dispatch, productsLoaded]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(fetchFilters())
    }, [dispatch, filtersLoaded]);

   if (!filtersLoaded) return <LoadingComponent message ='Loading products...'/>
    

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <Paper sx={{mb: 2}}>
                    <ProductSearch/>
                </Paper>
                <Paper sx={{mb: 2, p: 2}}>

                    Order by
                    <RadioButtonGroup
                        selectedValue={productParams.orderBy}
                        options={sortOption}
                        onChange={(e) => dispatch(setProductParams({orderBy: e.target.value}))}
                    />
                </Paper>

                <Paper sx={{mb: 2, p: 2}}>
                    Brand
                    <CheckBoxButton
                        items={brands}
                        checked={productParams.brands}
                        onChange={(item: string[]) => dispatch(setProductParams({brands: item}))}
                    />
                </Paper>
                <Paper sx={{mb: 2, p: 2}}>
                    Type
                    <CheckBoxButton
                        items={types}
                        checked={productParams.types}
                        onChange={(item: string[]) => dispatch(setProductParams({types: item}))}
                    />
                </Paper>
            </Grid>
            <Grid item xs={9}>
                <ProductList products={products}/>
            </Grid>
            <Grid item xs={3}/>
            <Grid item xs={9} sx ={{mb:2}}>
                {metaData && 
                    <AppPagination
                    metaData={metaData}
                onPageChange={(page: number) => dispatch(setPageNumber({pageNumber: page}))}
            />}
            </Grid>
        </Grid>
)
}