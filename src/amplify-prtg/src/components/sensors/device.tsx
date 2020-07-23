export interface Device{
    objId: Number,
    device: string,
}

export interface DeviceData {
    objId: Number,
    device: string,
    toners: Sensor,
    contadores: Sensor
}

export interface Sensor {
    sensorName : string,
    channels: Channel[],
}

export interface Channel {
    name: string,
    minimum: string,
    maximum: string,
    lastValue: string,
    info: ChannelInfo,
}

export interface ChannelInfo {
    data : ChannelInfoData[]
}

export interface ChannelInfoData {
    isPercent : boolean,
    average : string
}

export interface DeviceDataViewModel {
    objId: Number,
    thisMonthQuantityColorSheets: string,
    thisMonthQuantityBandWSheets: string,
    thisMonthQuantityTotalSheets: string,
    thisMonthQuantityCyanToners?: string,
    thisMonthQuantityYellowToners?: string,
    thisMonthQuantityMagentaToners?: string,
    thisMonthQuantityBlackToners?: string
}