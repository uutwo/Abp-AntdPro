import { Reducer } from 'redux';
import { Subscription, Effect } from 'dva';
import lodash from 'lodash';
import { UserLoginInfoDto } from '@/shared/dtos/appSession/userLoginInfoDto';
import { TenantLoginInfoDto } from '@/shared/dtos/appSession/tenantLoginInfoDto';
import { ApplicationInfoDto } from '@/shared/dtos/appSession/applicationInfoDto';
import { getCurrentLoginInformations } from "@/services/session/session";
import { getAll } from '@/services/abpUserConfiguration';
import getCurrentClockProvider from '@/shared/helpers/ClockProvider';
import moment from 'moment';
import { ListResultDto } from '@/shared/dtos/listResultDto';
import { UserLoginAttemptDto } from '@/services/userLogin/dtos/userLoginAttemptDto';
import UserLoginService from '@/services/userLogin/userLogin';
import { LocaleMappingService } from '@/shared/helpers/LocaleMappingService';
import { GetNotificationSettingsOutput } from '@/services/notification.ts/dtos/getNotificationSettingsOutput';
import NotificationService from '@/services/notification.ts/notification';
import ProfileService from '@/services/profile/profile';
export interface GlobalModelState {
  collapsed: boolean;
  user?: UserLoginInfoDto;
  tenant?: TenantLoginInfoDto;
  locales?:abp.localization.ILanguageInfo[];
  application?: ApplicationInfoDto | null;
  notificationSettingVisible?: boolean;
  loginRecordModal?: boolean;
  userLoginRecords?: ListResultDto<UserLoginAttemptDto>;
  notificationSetting?: GetNotificationSettingsOutput;
}

export interface GlobalModelType {
  namespace: 'global';
  state: GlobalModelState;
  effects: {
    getApplicationSession: Effect;
    initAbp: Effect;
    getRecentUserLoginAttempts: Effect;
    changeRecentUserLoginModalState: Effect;
    changeNotificationSettingModalState: Effect;
    getNotificationSettings: Effect;
    updateNotificationSettings: Effect;
    changeLanguage: Effect;
  };
  reducers: {
    savelocales: Reducer<GlobalModelState>;
    saveNotificationSettings: Reducer<GlobalModelState>;
    changeLayoutCollapsed: Reducer<GlobalModelState>;
    saveSessions: Reducer<GlobalModelState>;
    saveUserLoginRecents: Reducer<GlobalModelState>;
    saveRecentUserLoginModalState: Reducer<GlobalModelState>;
    saveNotificationSettingModalState: Reducer<GlobalModelState>;
  };
  subscriptions: { setup: Subscription };
}

const GlobalModel: GlobalModelType = {
  namespace: 'global',

  state: {
    collapsed: false,
    locales:[],
    application: null,
    loginRecordModal: false,
    notificationSettingVisible: false,
    notificationSetting: undefined
  },

  effects: {
    *initAbp(_, { call, put }) {
      const response = yield call(getAll);
      let result = response.result;
      lodash.merge(abp, result);
      abp.clock.provider = getCurrentClockProvider(result.clock.provider);
      moment.locale(new LocaleMappingService().map('moment', abp.localization.currentLanguage.name));
      if (abp.clock.provider.supportsMultipleTimezone) {
        (moment as any).tz.setDefault(abp.timing.timeZoneInfo.iana.timeZoneId);
        (window as any).moment.tz.setDefault(abp.timing.timeZoneInfo.iana.timeZoneId);
      }
      abp.event.trigger('abp.dynamicScriptsInitialized');

      yield put({
        type: 'savelocales',
        payload: result.localization.languages,
      });
    },
    *changeLanguage({ payload }, { call, put }) {
      yield call(ProfileService.changeLanguage, payload);
    },
    *getApplicationSession(_, { call, put }) {
      const sessionResponse = yield call(getCurrentLoginInformations);
      yield put({
        type: 'saveSessions',
        payload: sessionResponse.result,
      });
    },
    *changeRecentUserLoginModalState(_, { call, put }) {
      yield put({
        type: 'getRecentUserLoginAttempts',
      });
      yield put({
        type: 'saveRecentUserLoginModalState',
      });
    },
    *changeNotificationSettingModalState(_, { call, put }) {
      yield put({
        type: 'getNotificationSettings',
      });
      yield put({
        type: 'saveNotificationSettingModalState',
      });
    },
    *getRecentUserLoginAttempts(_, { call, put }) {
      const response = yield call(UserLoginService.getRecentUserLoginAttempts);
      yield put({
        type: 'saveUserLoginRecents',
        payload: response.result,
      });
    },
    *getNotificationSettings(_, { call, put }) {
      const response = yield call(NotificationService.getNotificationSettings);
      yield put({
        type: 'saveNotificationSettings',
        payload: response.result,
      });

    },
    *updateNotificationSettings({ payload }, { call, put }) {
      yield call(NotificationService.updateNotificationSettings, payload);
    },
  },

  reducers: {
    savelocales(state, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        locales: payload
      };
    },
    saveNotificationSettings(state, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        notificationSetting: payload
      };
    },
    saveRecentUserLoginModalState(state, { payload }): GlobalModelState {
      const isopen = state!.loginRecordModal;
      return {
        collapsed: false,
        ...state,
        loginRecordModal: !isopen
      };
    },
    saveNotificationSettingModalState(state, { payload }): GlobalModelState {
      const isopen = state!.notificationSettingVisible;
      return {
        collapsed: false,
        ...state,
        notificationSettingVisible: !isopen
      };
    },
    changeLayoutCollapsed(state = { collapsed: true }, { payload }): GlobalModelState {
      return {
        ...state,
        collapsed: payload,
      };
    },
    saveUserLoginRecents(state, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        userLoginRecords: payload,
      };
    },
    // 保存session
    saveSessions(state, { payload }): GlobalModelState {
      return {
        collapsed: false,
        ...state,
        application: payload.application,
        user: payload.user,
        tenant: payload.tenant
      };
    }
  },

  subscriptions: {
    setup({ history }): void {
      // Subscribe history(url) change, trigger `load` action if pathname is `/`
      history.listen(({ pathname, search }): void => {
        if (typeof window.ga !== 'undefined') {
          console.log(123)
          window.ga('send', 'pageview', pathname + search);
        }
      });
    },
  },
};

export default GlobalModel;
