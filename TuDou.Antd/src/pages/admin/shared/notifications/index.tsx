import AppComponentBase from "@/components/AppComponentBase";
import React, { Fragment } from "react";
import { Card, Button, Table, Divider, Icon } from "antd";
import { PageHeaderWrapper } from "@ant-design/pro-layout";
import { connect } from "dva";
import { ConnectState } from "@/models/connect";
import moment from "moment";
import { FormattedUserNotification } from "@/services/notification.ts/dtos/userNotification";
import ButtonGroup from "antd/lib/button/button-group";
import { Dispatch, AnyAction } from "redux";

export interface INotificationProps {
  notifications?: FormattedUserNotification[];
  loading: boolean;
  dispatch: Dispatch<AnyAction>;
}
@connect(({ notification, loading }: ConnectState) => ({
  notifications: notification.notifications,
  loading: loading.effects['notification/getUserNotifications'],
}))
class Notification extends AppComponentBase<INotificationProps> {
  formatNotification(record: any): string {
    return abp.utils.truncateStringWithPostfix(record, 120);
  }
  fromNow(date: string): string {
    return moment(date).fromNow();
  }

  openSettingModal=()=>{
    const { dispatch } = this.props;
    dispatch({
      type: "global/changeNotificationSettingModalState",
    })
  }
  async loadNotifications() {
    const { dispatch } = this.props;
    if (dispatch) {
      await dispatch({
        type: 'notification/getUserNotifications',
      });
    }
  }
  readAll=()=>{
    const { dispatch } = this.props;
    dispatch({
      type: "notification/setAllNotificationsAsRead",
    })
    this.notity.success({
      message:'操作成功!'
    })
    this.loadNotifications();
  }
  render() {
    const { notifications, loading } = this.props;
    const columns = [{
      title: '操作',
      width: "280px",
      dataIndex: 'action',
      key: 'action',
      render: (text: any, record: any, index: number) => {
        return <Fragment>
          <a href="#" ><Icon type="check" />忽略</a>
          <Divider type="vertical" />
          <a href="#"><Icon type="delete" />删除</a>
        </Fragment>
      }
    }, {
      title: '内容',
      dataIndex: 'text',
      key: 'text',
      render: (text: string, record: any, index: number) => {
        return <div>{this.formatNotification(text)}</div>
      }
    }, {
      title: '创建时间',
      dataIndex: 'creationTime',
      key: 'creationTime',
      render: (text: string, record: any, index: number) => {
        return <div>{this.fromNow(text)}</div>
      }
    }]
    return (
      <PageHeaderWrapper
        content="通知."
        extraContent={<ButtonGroup>

          <Button icon="setting" onClick={this.openSettingModal} type="default">通知设置</Button>
          <Button icon="check" onClick={this.readAll} type="primary">忽略全部</Button>

        </ButtonGroup>}>
        <Card>
          <Table
            bordered
            size="default"
            dataSource={notifications}
            loading={loading}
            columns={columns}
          />
        </Card>
      </PageHeaderWrapper>
    )
  }
}
export default Notification;
