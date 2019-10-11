import { Icon, Tooltip } from 'antd';
import React from 'react';
import { connect } from 'dva';
import { formatMessage } from 'umi-plugin-react/locale';
import { ConnectProps, ConnectState } from '@/models/connect';

import Avatar from './AvatarDropdown';
import HeaderSearch from '../HeaderSearch';
import SelectLang from '../SelectLang';
import styles from './index.less';
import LoginRecord from '../LoginRecord';

export type SiderTheme = 'light' | 'dark';
export interface GlobalHeaderRightProps extends ConnectProps {
  theme?: SiderTheme;
  layout: 'sidemenu' | 'topmenu';
  loginRecordModal:boolean;
}

const GlobalHeaderRight: React.SFC<GlobalHeaderRightProps> = props => {
  const { theme, layout,loginRecordModal,dispatch} = props;
  let className = styles.right;

  if (theme === 'dark' && layout === 'topmenu') {
    className = `${styles.right}  ${styles.dark}`;
  }
  const  loginRecordModalCancel=()=>{
    dispatch!({
      type:'global/changeRecentUserLoginModalState',
      payload:false
    })
  }
  return (
    <div className={className}>
    <LoginRecord onCancel={loginRecordModalCancel} visible={loginRecordModal}></LoginRecord>
      <HeaderSearch
        className={`${styles.action} ${styles.search}`}
        placeholder={formatMessage({
          id: 'component.globalHeader.search',
        })}
        dataSource={[
          formatMessage({
            id: 'component.globalHeader.search.example1',
          }),
          formatMessage({
            id: 'component.globalHeader.search.example2',
          }),
          formatMessage({
            id: 'component.globalHeader.search.example3',
          }),
        ]}
        onSearch={value => {
          console.log('input', value);
        }}
        onPressEnter={value => {
          console.log('enter', value);
        }}
      />
      <Tooltip
        title={formatMessage({
          id: 'component.globalHeader.help',
        })}
      >
        <a
          target="_blank"
          href="https://pro.ant.design/docs/getting-started"
          rel="noopener noreferrer"
          className={styles.action}
        >
          <Icon type="question-circle-o" />
        </a>
      </Tooltip>
      <Avatar />
      <SelectLang className={styles.action} />
    </div>
  );
};

export default connect(({ global,settings }: ConnectState) => ({
  loginRecordModal:global.loginRecordModal,
  theme: settings.navTheme,
  layout: settings.layout,
}))(GlobalHeaderRight);