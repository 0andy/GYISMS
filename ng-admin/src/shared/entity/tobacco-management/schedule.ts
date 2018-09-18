export class Schedule implements IScheduleom {
    id: string;
    desc: string;
    type: number;
    beginTime: string;
    endTime: string;
    status: number;
    publishTime: Date;
    isDeleted: boolean;
    creationTime: Date;
    creatorUserId: number;
    lastModificationTime: Date;
    lastModifierUserId: number;
    deletionTime: Date;
    deleterUserId: number;
    typeName: string;
    constructor(data?: IScheduleom) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.desc = data["desc"];
            this.type = data["type"];
            this.beginTime = data["beginTime"];
            this.endTime = data["endTime"];
            this.status = data["status"];
            this.publishTime = data["publishTime"];
            this.isDeleted = data["isDeleted"];
            this.creationTime = data["creationTime"];
            this.creatorUserId = data["creatorUserId"];
            this.lastModificationTime = data["lastModificationTime"];
            this.lastModifierUserId = data["lastModifierUserId"];
            this.deletionTime = data["deletionTime"];
            this.deleterUserId = data["deleterUserId"];
            this.creationTime = data["creationTime"];
            this.typeName = data["typeName"];
        }
    }

    static fromJS(data: any): Schedule {
        let result = new Schedule();
        result.init(data);
        return result;
    }

    static fromJSArray(dataArray: any[]): Schedule[] {
        let array = [];
        dataArray.forEach(result => {
            let item = new Schedule();
            item.init(result);
            array.push(item);
        });

        return array;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["desc"] = this.desc;
        data["type"] = this.type;
        data["beginTime"] = this.beginTime;
        data["endTime"] = this.endTime;
        data["status"] = this.status;
        data["publishTime"] = this.publishTime;
        data["isDeleted"] = this.isDeleted;
        data["creationTime"] = this.creationTime;
        data["creatorUserId"] = this.creatorUserId;
        data["lastModificationTime"] = this.lastModificationTime;
        data["lastModifierUserId"] = this.lastModifierUserId;
        data["deletionTime"] = this.deletionTime;
        data["deleterUserId"] = this.deleterUserId;
        data["typeName"] = this.typeName;
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new Schedule();
        result.init(json);
        return result;
    }
}
export interface IScheduleom {
    id: string;
    desc: string;
    type: number;
    beginTime: string;
    endTime: string;
    status: number;
    publishTime: Date;
    isDeleted: boolean;
    creationTime: Date;
    creatorUserId: number;
    lastModificationTime: Date;
    lastModifierUserId: number;
    deletionTime: Date;
    deleterUserId: number;
    typeName: string;
}