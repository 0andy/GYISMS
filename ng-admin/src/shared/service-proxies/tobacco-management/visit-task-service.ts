import 'rxjs/add/operator/finally';
import 'rxjs/add/operator/map';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, from as _observableFrom, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ApiResult } from '@shared/service-proxies/entity/parameter';
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';
import { NzTreeNode } from 'ng-zorro-antd';
import { GyismsHttpClient } from '@shared/service-proxies/gyisms-httpclient';
import { VisitTask, TaskExamine, ScheduleTask, VisitTaskName, VisitRecord } from '@shared/entity/tobacco-management';
import { GrowerAreaRecord } from '@shared/entity/basic-data';

@Injectable()
export class VisitTaskServiceProxy {
    private http: HttpClient;
    private _gyhttp: GyismsHttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Inject(GyismsHttpClient) gyhttp: GyismsHttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
        this._gyhttp = gyhttp;
    }
    getVisitTaskList(params: any): Observable<PagedResultDtoOfVisitTask> {
        let url_ = "/api/services/app/VisitTask/GetPagedVisitTasksAsync";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return PagedResultDtoOfVisitTask.fromJS(data);
            } else {
                return null;
            }
        });
    }

    getVisitVisitRecordListByGrowerId(params: any): Observable<PagedResultDtoOfVisitRecord> {
        let url_ = "/api/services/app/VisitRecord/GetVisitRecordsByGrowerId";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return PagedResultDtoOfVisitRecord.fromJS(data);
            } else {
                return null;
            }
        });
    }

    getGrowerAreaRecordListByGrowerId(params: any): Observable<PagedResultDtoOfGrowerAreaRecord> {
        let url_ = "/api/services/app/GrowerAreaRecord/GetPaged";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return PagedResultDtoOfGrowerAreaRecord.fromJS(data);
            } else {
                return null;
            }
        });
    }

    getVisitTaskListWithStatus(params: any): Observable<VisitTask[]> {
        let url_ = "/api/services/app/VisitTask/GetVisitTasksWithStatusAsync";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return VisitTask.fromJSArray(data);
            } else {
                return null;
            }
        });
    }

    getScheduleTaskListNoPage(id: string): Observable<ScheduleTask[]> {
        let url_ = "/api/services/app/ScheduleTask/GetScheduleTasksNoPageAsync?id=" + id;
        return this._gyhttp.get(url_).map(data => {
            if (data) {
                return ScheduleTask.fromJSArray(data);
            } else {
                return null;
            }
        });
    }

    getTaskExamineList(params: any): Observable<PagedResultDtoOfTaskExamine> {
        let url_ = "/api/services/app/TaskExamine/GetPagedTaskExaminesAsync";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return PagedResultDtoOfTaskExamine.fromJS(data);
            } else {
                return null;
            }
        });
    }

    getVisitTaskById(params: any): Observable<VisitTask> {
        let url_ = "/api/services/app/VisitTask/GetVisitTaskByIdAsync";
        return this._gyhttp.get(url_, params).map(data => {
            if (data) {
                return VisitTask.fromJS(data);
            } else {
                return null;
            }
        });
    }

    getTaskExamineById(id: number): Observable<TaskExamine> {
        let url_ = "/api/services/app/TaskExamine/GetTaskExamineByIdAsync?id=" + id;
        return this._gyhttp.get(url_).map(data => {
            if (data) {
                return TaskExamine.fromJS(data);
            } else {
                return null;
            }
        });
    }

    updateTaskInfo(input: any): Observable<any> {
        let url_ = "/api/services/app/VisitTask/CreateOrUpdateVisitTaskAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data;
        });
    }

    updateScheduleTask(input: any): Observable<any> {
        let url_ = "/api/services/app/ScheduleTask/CreateOrUpdateScheduleTaskAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data;
        });
    }

    updateScheduleDetail(input: any): Observable<any> {
        let url_ = "/api/services/app/ScheduleDetail/CreateOrUpdateScheduleTaskAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data;
        });
    }

    updateTaskExamineInfo(input: any): Observable<TaskExamine> {
        let url_ = "/api/services/app/TaskExamine/CreateOrUpdateTaskExamineAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data;
        });
    }

    deleteVisitTask(input: any): Observable<any> {
        let url_ = "/api/services/app/VisitTask/VisitTaskDeleteByIdAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data.result;
        });
    }

    deleteTaskExamine(input: any): Observable<any> {
        let url_ = "/api/services/app/TaskExamine/TaskExaminesDeleteByIdAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data.result;
        });
    }

    deleteScheduleTask(input: any): Observable<any> {
        let url_ = "/api/services/app/ScheduleTask/VisitTaskDeleteByIdAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data.result;
        });
    }

    createAllScheduleTask(input: any): Observable<ApiResult> {
        let url_ = "/api/services/app/ScheduleDetail/CreateAllScheduleTaskAsync";
        return this._gyhttp.post(url_, input).map(data => {
            return data.result;
        });
    }

    /**
     * 获取任务下拉框数据
     * @param params 
     */
    getTaskName(params: any): Observable<VisitTaskName[]> {
        var url = '/api/services/app/VisitTask/GetTaskList';
        return this._gyhttp.get(url, params).map(data => {
            if (data) {
                return VisitTaskName.fromJSArray(data)
            } else {
                return null;
            }
        });
    }
}

export class PagedResultDtoOfVisitTask implements IPagedResultDtoOfVisitTask {
    totalCount: number;
    items: VisitTask[];

    constructor(data?: IPagedResultDtoOfVisitTask) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.totalCount = data["totalCount"];
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [];
                for (let item of data["items"])
                    this.items.push(VisitTask.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PagedResultDtoOfVisitTask {
        let result = new PagedResultDtoOfVisitTask();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["totalCount"] = this.totalCount;
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new PagedResultDtoOfVisitTask();
        result.init(json);
        return result;
    }
}

export interface IPagedResultDtoOfVisitTask {
    totalCount: number;
    items: VisitTask[];
}

export class PagedResultDtoOfTaskExamine implements IPagedResultDtoOfTaskExamine {
    totalCount: number;
    items: TaskExamine[];

    constructor(data?: IPagedResultDtoOfTaskExamine) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.totalCount = data["totalCount"];
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [];
                for (let item of data["items"])
                    this.items.push(TaskExamine.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PagedResultDtoOfTaskExamine {
        let result = new PagedResultDtoOfTaskExamine();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["totalCount"] = this.totalCount;
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new PagedResultDtoOfTaskExamine();
        result.init(json);
        return result;
    }
}

export interface IPagedResultDtoOfTaskExamine {
    totalCount: number;
    items: TaskExamine[];
}

export class PagedResultDtoOfVisitRecord implements IPagedResultDtoOfVisitRecord {
    totalCount: number;
    items: VisitRecord[];

    constructor(data?: IPagedResultDtoOfVisitRecord) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.totalCount = data["totalCount"];
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [];
                for (let item of data["items"])
                    this.items.push(VisitRecord.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PagedResultDtoOfVisitRecord {
        let result = new PagedResultDtoOfVisitRecord();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["totalCount"] = this.totalCount;
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new PagedResultDtoOfVisitRecord();
        result.init(json);
        return result;
    }
}

export interface IPagedResultDtoOfVisitRecord {
    totalCount: number;
    items: VisitRecord[];
}

export class PagedResultDtoOfGrowerAreaRecord implements IPagedResultDtoOfGrowerAreaRecord {
    totalCount: number;
    items: GrowerAreaRecord[];

    constructor(data?: IPagedResultDtoOfGrowerAreaRecord) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.totalCount = data["totalCount"];
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [];
                for (let item of data["items"])
                    this.items.push(GrowerAreaRecord.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PagedResultDtoOfGrowerAreaRecord {
        let result = new PagedResultDtoOfGrowerAreaRecord();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["totalCount"] = this.totalCount;
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data;
    }

    clone() {
        const json = this.toJSON();
        let result = new PagedResultDtoOfGrowerAreaRecord();
        result.init(json);
        return result;
    }
}

export interface IPagedResultDtoOfGrowerAreaRecord {
    totalCount: number;
    items: GrowerAreaRecord[];
}