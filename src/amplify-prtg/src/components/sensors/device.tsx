export interface Device{
    objId: Number,
    device: string,
}

export interface DeviceData {
    objId: Number,
    device: string,
    sensorList : Sensor[],
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